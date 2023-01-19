using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RecipesFinder
{
    public class RecipeCollection
    {
        public ObservableCollection<Recipe> R { get; set; }
        
        public RecipeCollection()
        {
            R = new ObservableCollection<Recipe>();
        }

        public void addNewRecipe(Recipe recipe)
        {
            R.Add(recipe);
        }


        public void deleteRecipe(Recipe recipe)
        {
            R.Remove(recipe);
        }
    }
}
