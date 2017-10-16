/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 2.2.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class HopArrival :  IEquatable<HopArrival>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HopArrival" /> class.
        /// </summary>
        /// <param name="Code">The code of the &#x60;warehouse&#x60; or &#x60;truck&#x60;. (required).</param>
        /// <param name="DateTime">The date/time the parcel arrived at the hop. (required).</param>
        public HopArrival(string Code = default(string), DateTime? DateTime = default(DateTime?))
        {
            // to ensure "Code" is required (not null)
            if (Code == null)
            {
                throw new InvalidDataException("Code is a required property for HopArrival and cannot be null");
            }
            else
            {
                this.Code = Code;
            }
            // to ensure "DateTime" is required (not null)
            if (DateTime == null)
            {
                throw new InvalidDataException("DateTime is a required property for HopArrival and cannot be null");
            }
            else
            {
                this.DateTime = DateTime;
            }
            
        }

        /// <summary>
        /// The code of the &#x60;warehouse&#x60; or &#x60;truck&#x60;.
        /// </summary>
        /// <value>The code of the &#x60;warehouse&#x60; or &#x60;truck&#x60;.</value>
        [DataMember(Name="code")]
        public string Code { get; set; }
        /// <summary>
        /// The date/time the parcel arrived at the hop.
        /// </summary>
        /// <value>The date/time the parcel arrived at the hop.</value>
        [DataMember(Name="dateTime")]
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class HopArrival {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  DateTime: ").Append(DateTime).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((HopArrival)obj);
        }

        /// <summary>
        /// Returns true if HopArrival instances are equal
        /// </summary>
        /// <param name="other">Instance of HopArrival to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(HopArrival other)
        {

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    this.Code == other.Code ||
                    this.Code != null &&
                    this.Code.Equals(other.Code)
                ) && 
                (
                    this.DateTime == other.DateTime ||
                    this.DateTime != null &&
                    this.DateTime.Equals(other.DateTime)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                    if (this.Code != null)
                    hash = hash * 59 + this.Code.GetHashCode();
                    if (this.DateTime != null)
                    hash = hash * 59 + this.DateTime.GetHashCode();
                return hash;
            }
        }

        #region Operators

        public static bool operator ==(HopArrival left, HopArrival right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HopArrival left, HopArrival right)
        {
            return !Equals(left, right);
        }

        #endregion Operators

    }
}
