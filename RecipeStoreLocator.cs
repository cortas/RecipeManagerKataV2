using System;
using System.IO;

namespace RecipeManager
{
    public class RecipeStoreLocator
    {
        public string GetRecipeDirectory()
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "RecipeMaker");

            if (File.Exists(directory + @"\" + "RecipeDirectory.txt"))
            {
                directory = File.ReadAllText(directory + @"\" + "RecipeDirectory.txt");
            }
            else
            {
                directory += @"\RecipeDirectory";
            }

            return directory;
        }

        public void SetRecipeDirectory(string recipeDirectory)
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "RecipeMaker");

            File.WriteAllText(directory + @"\" + "RecipeDirectory.txt", recipeDirectory);
        }
    }
}