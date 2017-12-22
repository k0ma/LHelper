namespace LHelper.Services.Admin.Models
{
    using LHelper.Common.Mapping;
    using LHelper.Data.Models;
    using System.Collections.Generic;

    public class CategoryDetailsServiceModel : CategoryBasicDetailsServiceModel
    {
       // public List<UserCategory> Trainers { get; set; } = new List<UserCategory>();

        public IEnumerable<string> SelectedTrainers { get; set; }

    }
}
