using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipesFinder
{
    public class Recipe
    {
        [PrimaryKey]
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int likes { get; set; }

        

        public Recipe()
        {
        }

    }
}
