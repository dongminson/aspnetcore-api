using Microsoft.EntityFrameworkCore;

namespace aspnetcore_api.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DefinitionEntry>().HasData(
                new DefinitionEntry { Id = 1, PartOfSpeech = "noun", Definition = "a professionally trained person who makes and serves coffee in a coffee bar", WordId = 1 },
                new DefinitionEntry { Id = 2, PartOfSpeech = "noun", Definition = "A Japanese stringed instrument usually having 13 silk strings stretched over a long, hollow, wooden body.", WordId = 2 },
                new DefinitionEntry { Id = 3, PartOfSpeech = "noun", Definition = "A usually spicy Korean dish made of vegetables, such as cabbage or radishes, that are salted, seasoned, and allowed to ferment.", WordId = 3 }
            );

            modelBuilder.Entity<WordEntry>().HasData(
                new WordEntry { Id = 1, Word = "barisa" },
                new WordEntry { Id = 2, Word = "koto" },
                new WordEntry { Id = 3, Word = "kimchi" }
            );

        }
    }
}
