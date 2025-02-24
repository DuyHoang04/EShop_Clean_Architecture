using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Models.Product>().AnyAsync()) return;

            session.Store<Models.Product>(GetDataProduct());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Models.Product> GetDataProduct() => new List<Models.Product>
        {

        };
    }
}
