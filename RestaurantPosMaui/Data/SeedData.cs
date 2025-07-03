using MenuItem = RestaurantPosMaui.Data.MenuItem;

namespace RestaurantPosMaui.Data;
class SeedData
{
    public static List<MenuCategory> GetMenuCategories() {
        return new List<MenuCategory>
        {
            new MenuCategory { Id = 1, Name = "Beverages", Icon = "drink.png" },
            new MenuCategory { Id = 2, Name = "Main Course", Icon = "meal.png" },
            new MenuCategory { Id = 3, Name = "Snacks", Icon = "snacks.png" },
            new MenuCategory { Id = 4, Name = "Desserts", Icon = "cake.png" },
            new MenuCategory { Id = 5, Name = "Fast Food", Icon = "fast_food.png" }
        };
    }

    public static List<MenuItem> GetMenuItems()
    {
        return new List<MenuItem>
        {
            new MenuItem { Id = 1, Name = "Beer", Icon = "beer.png", Description = "Chilled beer for a relaxing time.", Price = 500 },
            new MenuItem { Id = 2, Name = "Biryani", Icon = "biyani.png", Description = "Spicy and flavorful rice dish.", Price = 750 },
            new MenuItem { Id = 3, Name = "Buns", Icon = "buns.png", Description = "Soft and fluffy bread rolls, perfect as a snack or side.", Price = 200 },
            new MenuItem { Id = 4, Name = "Burger Combo", Icon = "burger_fries_combo.png", Description = "Burger with crispy fries.", Price = 950 },
            new MenuItem { Id = 5, Name = "Cake", Icon = "cake.png", Description = "Delicious layered dessert.", Price = 400 },
            new MenuItem { Id = 6, Name = "Chocolate", Icon = "chocolate.png", Description = "Rich and smooth chocolate.", Price = 300 },
            new MenuItem { Id = 7, Name = "Cocktail", Icon = "cocktail.png", Description = "Colorful refreshing drink.", Price = 600 },
            new MenuItem { Id = 8, Name = "Coffee", Icon = "coffee.png", Description = "Freshly brewed hot coffee.", Price = 250 },
            new MenuItem { Id = 9, Name = "Cupcake", Icon = "cupcake.png", Description = "Sweet and fluffy cupcake.", Price = 200 },
            new MenuItem { Id = 10, Name = "Donut", Icon = "donut.png", Description = "Classic glazed donut.", Price = 150 },
            new MenuItem { Id = 11, Name = "Energy Drink", Icon = "energy_drink.png", Description = "Boost your energy instantly.", Price = 350 },
            new MenuItem { Id = 12, Name = "Fish", Icon = "fish.png", Description = "Freshly cooked fish dish.", Price = 700 },
            new MenuItem { Id = 13, Name = "Fish and Chips", Icon = "fish_and_chips.png", Description = "British-style crispy delight.", Price = 850 },
            new MenuItem { Id = 14, Name = "French Fries", Icon = "french_fries.png", Description = "Crispy golden potato fries.", Price = 300 },
            new MenuItem { Id = 15, Name = "Fried Chicken", Icon = "fried_chicken.png", Description = "Crispy and juicy chicken.", Price = 750 },
            new MenuItem { Id = 16, Name = "Fried Egg", Icon = "fried_egg.png", Description = "Sunny-side-up fried egg.", Price = 100 },
            new MenuItem { Id = 17, Name = "Fried Rice", Icon = "fried_rice.png", Description = "Stir-fried rice with veggies.", Price = 600 },
            new MenuItem { Id = 18, Name = "Hamburger", Icon = "hamburger.png", Description = "Juicy grilled beef burger.", Price = 700 },
            new MenuItem { Id = 19, Name = "Hotdog", Icon = "hotdog.png", Description = "Classic sausage in a bun.", Price = 550 },
            new MenuItem { Id = 20, Name = "Ice Cream", Icon = "ice_cream.png", Description = "Cold and creamy dessert.", Price = 350 },
            new MenuItem { Id = 21, Name = "Idli Platter", Icon = "idli_platter.png", Description = "South Indian rice cakes.", Price = 400 },
            new MenuItem { Id = 22, Name = "Kebab", Icon = "kebab.png", Description = "Spicy grilled meat skewers.", Price = 800 },
            new MenuItem { Id = 23, Name = "Kimchi Jjigae", Icon = "kimchi_jjigae.png", Description = "Korean spicy stew.", Price = 850 },
            new MenuItem { Id = 24, Name = "Laddu", Icon = "laddu.png", Description = "Sweet Indian festive treat.", Price = 250 },
            new MenuItem { Id = 25, Name = "Lobster", Icon = "lobster.png", Description = "Luxurious seafood delicacy.", Price = 1500 },
            new MenuItem { Id = 26, Name = "Mango", Icon = "mango.png", Description = "Juicy tropical fruit.", Price = 300 },
            new MenuItem { Id = 27, Name = "Masala Dosa", Icon = "masala_dosa.png", Description = "Crispy dosa with potato filling.", Price = 500 },
            new MenuItem { Id = 28, Name = "Nachos", Icon = "nachos.png", Description = "Corn chips with cheesy dip.", Price = 400 },
            new MenuItem { Id = 29, Name = "Noodles", Icon = "noodles.png", Description = "Stir-fried Asian noodles.", Price = 550 },
            new MenuItem { Id = 30, Name = "Orange Juice", Icon = "orange_juice.png", Description = "Freshly squeezed citrus juice.", Price = 300 },
            new MenuItem { Id = 31, Name = "Pancakes", Icon = "pancakes.png", Description = "Fluffy stack with syrup.", Price = 450 },
            new MenuItem { Id = 32, Name = "Paneer", Icon = "paneer.png", Description = "Indian cottage cheese dish.", Price = 600 },
            new MenuItem { Id = 33, Name = "Pasta", Icon = "pasta.png", Description = "Creamy Italian-style pasta.", Price = 700 },
            new MenuItem { Id = 34, Name = "Pie", Icon = "pie.png", Description = "Savory or sweet baked dish.", Price = 400 },
            new MenuItem { Id = 35, Name = "Pizza", Icon = "pizza.png", Description = "Cheesy and topping-loaded pizza.", Price = 850 },
            new MenuItem { Id = 36, Name = "Pizza Slice", Icon = "pizza_slice.png", Description = "A single slice of pizza heaven.", Price = 250 },
            new MenuItem { Id = 37, Name = "Ramen", Icon = "ramen.png", Description = "Japanese noodle soup.", Price = 650 },
            new MenuItem { Id = 38, Name = "Rice", Icon = "rice.png", Description = "Steamed white rice.", Price = 300 },
            new MenuItem { Id = 39, Name = "Roasted Chicken", Icon = "roasted_chicken.png", Description = "Oven-roasted juicy chicken.", Price = 800 },
            new MenuItem { Id = 40, Name = "Salad Bowl", Icon = "salad_bowl.png", Description = "Healthy mixed greens.", Price = 450 },
            new MenuItem { Id = 41, Name = "Salad Plate", Icon = "salad_plate.png", Description = "A classic fresh salad platter.", Price = 400 },
            new MenuItem { Id = 42, Name = "Samosa", Icon = "samosa.png", Description = "Spicy Indian snack.", Price = 200 },
            new MenuItem { Id = 43, Name = "Sandwich", Icon = "sandwich.png", Description = "Layered bread with fillings.", Price = 400 },
            new MenuItem { Id = 44, Name = "Soda", Icon = "soda.png", Description = "Carbonated soft drink.", Price = 150 },
            new MenuItem { Id = 45, Name = "Soju", Icon = "soju.png", Description = "Popular Korean alcohol.", Price = 550 },
            new MenuItem { Id = 46, Name = "Spaghetti", Icon = "spaghetti.png", Description = "Italian pasta with sauce.", Price = 700 },
            new MenuItem { Id = 47, Name = "Sushi", Icon = "sushi.png", Description = "Japanese vinegared rice roll.", Price = 950 },
            new MenuItem { Id = 48, Name = "Taco", Icon = "taco.png", Description = "Mexican stuffed tortilla.", Price = 500 },
            new MenuItem { Id = 49, Name = "Thai Food", Icon = "thai_food.png", Description = "Authentic Thai cuisine.", Price = 800 },
            new MenuItem { Id = 50, Name = "Thali", Icon = "thali.png", Description = "Full Indian meal platter.", Price = 850 },
            new MenuItem { Id = 51, Name = "Wrap", Icon = "wrap.png", Description = "Rolled sandwich wrap.", Price = 450 }


        };
    }

    public static List<MenuItemCategoryMapping> GetMenuItemCategoryMapping()
    {
        return new List<MenuItemCategoryMapping>
    {
        // Beverages (ID = 1)
        new MenuItemCategoryMapping { MenuCategoryId = 1, MenuItemId = 1 },
        new MenuItemCategoryMapping { MenuCategoryId = 1, MenuItemId = 7 },
        new MenuItemCategoryMapping { MenuCategoryId = 1, MenuItemId = 8 },
        new MenuItemCategoryMapping { MenuCategoryId = 1, MenuItemId = 11 },
        new MenuItemCategoryMapping { MenuCategoryId = 1, MenuItemId = 30 },
        new MenuItemCategoryMapping { MenuCategoryId = 1, MenuItemId = 44 },
        new MenuItemCategoryMapping { MenuCategoryId = 1, MenuItemId = 45 },

        // Desserts (ID = 4)
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 5 },
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 6 },
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 9 },
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 10 },
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 20 },
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 24 },
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 26 },
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 31 },
        new MenuItemCategoryMapping { MenuCategoryId = 4, MenuItemId = 34 },

        // Main Course (ID = 2)
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 2 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 12 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 13 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 17 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 18 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 22 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 23 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 25 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 27 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 29 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 32 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 33 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 35 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 36 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 37 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 38 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 39 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 46 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 49 },
        new MenuItemCategoryMapping { MenuCategoryId = 2, MenuItemId = 50 },

        // Fast Food (ID = 5)
        new MenuItemCategoryMapping { MenuCategoryId = 5, MenuItemId = 4 },
        new MenuItemCategoryMapping { MenuCategoryId = 5, MenuItemId = 14 },
        new MenuItemCategoryMapping { MenuCategoryId = 5, MenuItemId = 15 },
        new MenuItemCategoryMapping { MenuCategoryId = 5, MenuItemId = 16 },
        new MenuItemCategoryMapping { MenuCategoryId = 5, MenuItemId = 19 },
        new MenuItemCategoryMapping { MenuCategoryId = 5, MenuItemId = 43 },
        new MenuItemCategoryMapping { MenuCategoryId = 5, MenuItemId = 48 },
        new MenuItemCategoryMapping { MenuCategoryId = 5, MenuItemId = 51 },

        // Snacks (ID = 3)
        new MenuItemCategoryMapping { MenuCategoryId = 3, MenuItemId = 3 },
        new MenuItemCategoryMapping { MenuCategoryId = 3, MenuItemId = 21 },
        new MenuItemCategoryMapping { MenuCategoryId = 3, MenuItemId = 28 },
        new MenuItemCategoryMapping { MenuCategoryId = 3, MenuItemId = 40 },
        new MenuItemCategoryMapping { MenuCategoryId = 3, MenuItemId = 41 },
        new MenuItemCategoryMapping { MenuCategoryId = 3, MenuItemId = 42 }
    };
    }


}