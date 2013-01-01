using System;
using System.Linq.Expressions;
using Raven.Abstractions.Exceptions;
using Raven.Client;
using Raven.Json.Linq;

namespace CAHM.Raven
{
    public static class UniqueConstraintExtensions
    {

        public class UniqueConstraint
        {
            public string Type { get; set; }
            public string Key { get; set; }
        }

        public static void StoreUniqueWithExpiration<TEntity, TUnique>(
            this IDocumentSession session,
            TEntity entity,
            Expression<Func<TEntity, TUnique>> keyProperty, 
            DateTime expiration)
        {
            session.StoreUnique(entity, keyProperty,
                                e => session.SetExpiration(e, expiration),
                                c => session.SetExpiration(c, expiration));
        }

        public static void StoreUnique<TEntity, TUnique>(
            this IDocumentSession session,
            TEntity entity,
            Expression<Func<TEntity, TUnique>> keyProperty)
        {
            session.StoreUnique(entity, keyProperty, e => { }, c => { });
        }

        public static void StoreUnique<TEntity, TUnique>(
            this IDocumentSession session,
            TEntity entity,
            Expression<Func<TEntity, TUnique>> keyProperty,
            Action<TEntity> onSaveEntity, Action<UniqueConstraint> onSaveConstraint)
        {
            if (session == null)
                throw new ArgumentNullException("session");
            if (ReferenceEquals(entity, null))
                throw new ArgumentNullException("entity");
            if (keyProperty == null)
                throw new ArgumentNullException("keyProperty");

            var key = keyProperty.Compile().Invoke(entity).ToString();

            var constraint = new UniqueConstraint
            {
                Type = typeof(TEntity).Name,
                Key = key
            };

            Store(session, entity, constraint, () => onSaveEntity(entity), () => onSaveConstraint(constraint));
        }


        private static void Store(IDocumentSession session, object entity, UniqueConstraint constraint,
                                  Action onSaveEntity, Action onSaveConstraint)
        {
            var id = string.Format("UniqueConstraints/{0}/{1}", constraint.Type, constraint.Key);

            var previousSetting = session.Advanced.UseOptimisticConcurrency;

            try
            {
                session.Advanced.UseOptimisticConcurrency = true;
                session.Store(constraint, id);
                session.Store(entity);

                onSaveEntity();
                onSaveConstraint();

                session.SaveChanges();
            }
            catch (ConcurrencyException)
            {
                session.Advanced.Evict(entity);
                session.Advanced.Evict(constraint);
                throw;
            }
            finally
            {
                session.Advanced.UseOptimisticConcurrency = previousSetting;
            }
        }

        private static void SetExpiration<TEntity>(this IDocumentSession session, TEntity entity, DateTime expiration)
        {
            if (expiration.Kind == DateTimeKind.Local)
                expiration = expiration.ToUniversalTime();

            var metadata = session.Advanced.GetMetadataFor(entity);
            metadata["Raven-Expiration-Date"] = new RavenJValue(expiration);
        }

    }
}
