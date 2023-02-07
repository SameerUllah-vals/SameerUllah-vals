using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class DynamicFormMeta
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public List<Input> Inputs { get; set; } = new List<Input>();
    }
    
    public class AttributeModel
    {
        public int Id { get; set; }

        public int DynamicFormInputId { get; set; }

        public string AttrKey { get; set; } = null!;

        public string AttrValue { get; set; } = null!;
    }

    public class Input
    {
        public int Id { get; set; }

        public int DynamicFormId { get; set; }

        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public bool IsRequired { get; set; }

        public int SequenceOrder { get; set; }

        public List<AttributeModel> Attributes { get; set; } = new List<AttributeModel>();

        public List<DynamicFormInputDataSource> DropdownOpt { get; set; } = new List<DynamicFormInputDataSource>();

    }

    public class Grid
    {
        public string Data { get; set; } = "Id";
        public bool searchable { get; set; } = false;
        public bool bSortable { get; set; } = true;
        public bool bVisible { get; set; } = false;

        
    }

    public class GridDetails
    {
        public string Data { get; set; }
    }

    public class DynamicFormMasterMeta
    {
        public DynamicForm DynamicForm { get; set; }
        public DynamicFormMaster DynamicFormMaster { get; set; }
    }

}
