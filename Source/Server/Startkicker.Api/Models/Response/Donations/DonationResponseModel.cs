namespace Startkicker.Api.Models.Response.Donations
{
    using System;
    using System.Linq.Expressions;

    using Startkicker.Data.Models;

    public class DonationResponseModel
    {
        public static Expression<Func<Donation, DonationResponseModel>> FromModel
        {
            get
            {
                return d => new DonationResponseModel
                {
                    User = d.User.UserName,
                    Project = d.Project.Name,
                    Ammount = d.Ammount.ToString()
                };
            }
        }

        public string User { get; set; }

        public string Project { get; set; }

        public string Ammount { get; set; }
    }
}