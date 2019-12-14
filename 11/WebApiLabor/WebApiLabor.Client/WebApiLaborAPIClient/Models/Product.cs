﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace WebApiLaborAPI.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Product
    {
        /// <summary>
        /// Initializes a new instance of the Product class.
        /// </summary>
        public Product() { }

        /// <summary>
        /// Initializes a new instance of the Product class.
        /// </summary>
        public Product(int? id = default(int?), string name = default(string), int? unitPrice = default(int?), string shipmentRegion = default(string), int? categoryId = default(int?), Category category = default(Category), IList<Order> orders = default(IList<Order>))
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            ShipmentRegion = shipmentRegion;
            CategoryId = categoryId;
            Category = category;
            Orders = orders;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "unitPrice")]
        public int? UnitPrice { get; set; }

        /// <summary>
        /// Possible values include: 'EU', 'NorthAmerica', 'Asia', 'Australia'
        /// </summary>
        [JsonProperty(PropertyName = "shipmentRegion")]
        public string ShipmentRegion { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "categoryId")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "category")]
        public Category Category { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "orders")]
        public IList<Order> Orders { get; set; }

    }
}