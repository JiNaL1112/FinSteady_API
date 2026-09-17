using FinSteady_API.Infrastructure;
using FinSteady_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinSteady_API.Repositories
{

    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        private readonly SmartSaverDatabaseContext databaseContext;

        public CardRepository(SmartSaverDatabaseContext smartSaverDatabaseContext)
        : base(smartSaverDatabaseContext)
        {
            this.databaseContext = smartSaverDatabaseContext;
        }

        public async Task<IEnumerable<Card>> GetCards()
        {
            return await this.Find()
            .OrderByDescending(a => a.CardId)
            .ToListAsync();
        }

        public async Task<Card> GetCardById(int id)
        {
            return await this.Find(d => d.CardId == id)
            .SingleOrDefaultAsync();
        }

        public async Task<Card> AddCard(Card Card)
        {

            this.CreateEntity(Card);

            await this.SaveAsync();

            return Card;
        }

        public async Task<Card> UpdateCard(Card dbCard, Card Card)
        {
            Card.CardId = dbCard.CardId;


            dbCard.Map(Card);
            this.UpdateEntity(dbCard);

            await this.SaveAsync();
            return dbCard;
        }

        public async Task DeleteCard(Card Card)
        {
            this.DeleteEntity(Card);

            await this.SaveAsync();
        }

    }

}


