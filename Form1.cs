// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="">
//   
// </copyright>
// <summary>
//   The form 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeManager
{
    /// <summary>
    /// The form 1.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The m_recipes.
        /// </summary>
        private List<Recipe> m_recipes = new List<Recipe>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            LoadRecipes();
            textBoxRecipeDirectory.Text = GetRecipeDirectory();
        }

        /// <summary>
        /// The get recipe directory.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetRecipeDirectory()
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RecipeMaker");

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

        /// <summary>
        /// The set recipe directory.
        /// </summary>
        /// <param name="recipeDirectory">
        /// The recipe directory.
        /// </param>
        private void SetRecipeDirectory(string recipeDirectory)
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RecipeMaker");

            File.WriteAllText(directory + @"\" + "RecipeDirectory.txt", recipeDirectory);
        }

        /// <summary>
        /// The load recipes.
        /// </summary>
        private void LoadRecipes()
        {
            string directory = GetRecipeDirectory();
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            directoryInfo.Create();

            m_recipes = directoryInfo.GetFiles("*")
                .Select(fileInfo => new Recipe { Name = fileInfo.Name, Size = fileInfo.Length, Text = File.ReadAllText(fileInfo.FullName) }).ToList();

            PopulateList();
        }

        /// <summary>
        /// The populate list.
        /// </summary>
        private void PopulateList()
        {
            listView1.Items.Clear();

            foreach (Recipe recipe in m_recipes)
            {
                listView1.Items.Add(new RecipeListViewItem(recipe));
            }
        }

        /// <summary>
        /// The delete click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DeleteClick(object sender, EventArgs e)
        {
            foreach (RecipeListViewItem recipeListViewItem in listView1.SelectedItems)
            {
                m_recipes.Remove(recipeListViewItem.Recipe);
                string directory = GetRecipeDirectory();

                File.Delete(directory + @"\" + recipeListViewItem.Recipe.Name);
            }

            PopulateList();

            NewClick(null, null);
        }

        /// <summary>
        /// The new click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void NewClick(object sender, EventArgs e)
        {
            textBoxName.Text = string.Empty;
            textBoxObjectData.Text = string.Empty;
        }

        /// <summary>
        /// The save click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SaveClick(object sender, EventArgs e)
        {
            string directory = GetRecipeDirectory();

            File.WriteAllText(Path.Combine(directory, textBoxName.Text), textBoxObjectData.Text);
            LoadRecipes();
        }

        /// <summary>
        /// The selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (RecipeListViewItem recipeListViewItem in listView1.SelectedItems)
            {
                textBoxName.Text = recipeListViewItem.Recipe.Name;
                textBoxObjectData.Text = recipeListViewItem.Recipe.Text;
                break;
            }
        }

        /// <summary>
        /// The button save recipe directory_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void buttonSaveRecipeDirectory_Click(object sender, EventArgs e)
        {
            SetRecipeDirectory(textBoxRecipeDirectory.Text);
            LoadRecipes();
            NewClick(null, null);
        }
    }
}
