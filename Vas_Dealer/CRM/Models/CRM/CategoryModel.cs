using System.Collections.Generic;

namespace VAS.Dealer.Models.CRM
{
    public class CategoryModel
    {
        public int CatTypeId { get; set; }
        public string CatTypeName { get; set; }
        public string CatTypeGroup { get; set; }

        public DataForCategoryTree data
        {
            get =>
                new DataForCategoryTree()
                {
                    CatTypeId = CatTypeId,
                    CatTypeGroup = CatTypeGroup,
                    CatTypeName = CatTypeName
                };
        }
        public StateCategoryTree state { get; set; }

        public string text { get; set; }
        public string icon { get; set; }
        public bool HasChild { get; set; }
        public List<CategoryModel> children { get; set; }

    }

    public class StateCategoryTree
    {
        public bool opened { get; set; }
        public bool selected { get; set; }
    }
    public class DataForCategoryTree
    {
        public int CatTypeId { get; set; }
        public string CatTypeName { get; set; }
        public string CatTypeGroup { get; set; }
    }

    public class CategoryItemModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string ExpandProperties { get; set; }
    }


    public class CategoryGroupSelectModel
    {
        public string Group { get; set; }
        public string DataValueFeld { get; set; }
        public string DataTextFeld { get; set; }
    }


    public class ProductDetialByModelModel
    {
        public List<Select2Model> Select2Data { get; set; }
        /// <summary>
        /// Dung tích/ công xuất
        /// </summary>
        public string ProductCapacity { get; set; }
    }
}
