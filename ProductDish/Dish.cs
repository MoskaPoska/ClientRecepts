namespace ProductDish
{
    public class Dish
    {
        public string NameDish { get; set; }
        public List<Dish> listDishes { get; set; }
        public List<Product> products { get; set; }
        public Dish() { }
        public void Menu()
        {
            products = new List<Product>();
            products.Add(new Product() {ProductName = "Буряк"});
            products.Add(new Product() {ProductName = "Картошка" });
            products.Add(new Product() {ProductName = "Капуста" });
            products.Add(new Product() {ProductName = "Морковка" });
            products.Add(new Product() {ProductName = "Курица" });
            products.Add(new Product() {ProductName = "Яйцо" });
            products.Add(new Product() {ProductName = "Соль" });
            products.Add(new Product() {ProductName = "Овсянка" });
            products.Add(new Product() {ProductName = "Молоко" });
            products.Add(new Product() {ProductName = "Сахар" });

            listDishes = new List<Dish>();
            listDishes.Add(new Dish() { NameDish = "Борщ" });
            listDishes.Add(new Dish() { NameDish = "Рагу" });
            listDishes.Add(new Dish() { NameDish = "Яичница" });
            listDishes.Add(new Dish() { NameDish = "Овсянка с молоком" });
        }
        public bool Borsh(List<Product> products)
        {
            var result = products.Where(t => t.ProductName == "Буряк" && t.ProductName == "Картошка" && t.ProductName == "Капуста" && t.ProductName == "Морковка" && t.ProductName == "Курица" && t.ProductName == "");
            if(result.Count() == 5)
                return true;
            else
                return false;   
        }
        public bool Ragu(List<Product> products)
        {
            var result = products.Where(t => t.ProductName == "Картошка" && t.ProductName == "Капуста" && t.ProductName == "Морковка" && t.ProductName == "");
            if (result.Count() == 4)
                return true;
            return false;
        }
        public bool FriedEggs(List<Product> products)
        {
            var result = products.Where(t => t.ProductName == "Яйцо" && t.ProductName == "Соль");
            if(result.Count()==2)
                return true;
            return false;
        }
        public bool Oatmeal(List<Product> products)
        {
            var result = products.Where(t => t.ProductName == "Овсянка" && t.ProductName == "Молоко" && t.ProductName == "Сахар");
            if(result.Count()==3)
                return true;
            return false;
        }
    }
}