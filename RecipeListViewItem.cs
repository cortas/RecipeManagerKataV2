// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipeListViewItem.cs" company="">
//   
// </copyright>
// <summary>
//   The recipe list view item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeManager
{
    /// <summary>
    /// The recipe list view item.
    /// </summary>
    class RecipeListViewItem: ListViewItem
    {
        /// <summary>
        /// The m_recipe.
        /// </summary>
        private Recipe m_recipe;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeListViewItem"/> class.
        /// </summary>
        /// <param name="recipe">
        /// The recipe.
        /// </param>
        public RecipeListViewItem(Recipe recipe)
        {
            m_recipe = recipe;

            Text = recipe.Name;
            SubItems.Add(new ListViewSubItem(this, m_recipe.Size.ToString()));
        }

        /// <summary>
        /// Gets the recipe.
        /// </summary>
        public Recipe Recipe {
            get { return m_recipe; }
        }
    }
}
