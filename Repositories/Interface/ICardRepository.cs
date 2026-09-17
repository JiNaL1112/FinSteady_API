using FinSteady_API.Infrastructure;

namespace FinSteady_API.Repositories.Interface
{
    public interface ICardRepository
    {

        Task<IEnumerable<Card>> GetCards();

        Task<Card> GetCardById(int id);

        Task<Card> AddCard(Card Card);

        Task<Card> UpdateCard(Card dbCard, Card Card);

        Task DeleteCard(Card Card);
    }
}
