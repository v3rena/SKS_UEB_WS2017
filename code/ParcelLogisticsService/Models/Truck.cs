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
    public partial class Truck :  IEquatable<Truck>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Truck" /> class.
        /// </summary>
        /// <param name="Code">Code (required).</param>
        /// <param name="NumberPlate">NumberPlate (required).</param>
        /// <param name="Latitude">Latitude (required).</param>
        /// <param name="Longitude">Longitude (required).</param>
        /// <param name="Radius">Radius (required).</param>
        /// <param name="Duration">Duration (required).</param>
        public Truck(string Code = default(string), string NumberPlate = default(string), decimal? Latitude = default(decimal?), decimal? Longitude = default(decimal?), decimal? Radius = default(decimal?), decimal? Duration = default(decimal?))
        {
            // to ensure "Code" is required (not null)
            if (Code == null)
            {
                throw new InvalidDataException("Code is a required property for Truck and cannot be null");
            }
            else
            {
                this.Code = Code;
            }
            // to ensure "NumberPlate" is required (not null)
            if (NumberPlate == null)
            {
                throw new InvalidDataException("NumberPlate is a required property for Truck and cannot be null");
            }
            else
            {
                this.NumberPlate = NumberPlate;
            }
            // to ensure "Latitude" is required (not null)
            if (Latitude == null)
            {
                throw new InvalidDataException("Latitude is a required property for Truck and cannot be null");
            }
            else
            {
                this.Latitude = Latitude;
            }
            // to ensure "Longitude" is required (not null)
            if (Longitude == null)
            {
                throw new InvalidDataException("Longitude is a required property for Truck and cannot be null");
            }
            else
            {
                this.Longitude = Longitude;
            }
            // to ensure "Radius" is required (not null)
            if (Radius == null)
            {
                throw new InvalidDataException("Radius is a required property for Truck and cannot be null");
            }
            else
            {
                this.Radius = Radius;
            }
            // to ensure "Duration" is required (not null)
            if (Duration == null)
            {
                throw new InvalidDataException("Duration is a required property for Truck and cannot be null");
            }
            else
            {
                this.Duration = Duration;
            }
            
        }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name="code")]
        public string Code { get; set; }
        /// <summary>
        /// Gets or Sets NumberPlate
        /// </summary>
        [DataMember(Name="numberPlate")]
        public string NumberPlate { get; set; }
        /// <summary>
        /// Gets or Sets Latitude
        /// </summary>
        [DataMember(Name="latitude")]
        public decimal? Latitude { get; set; }
        /// <summary>
        /// Gets or Sets Longitude
        /// </summary>
        [DataMember(Name="longitude")]
        public decimal? Longitude { get; set; }
        /// <summary>
        /// Gets or Sets Radius
        /// </summary>
        [DataMember(Name="radius")]
        public decimal? Radius { get; set; }
        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        [DataMember(Name="duration")]
        public decimal? Duration { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Truck {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  NumberPlate: ").Append(NumberPlate).Append("\n");
            sb.Append("  Latitude: ").Append(Latitude).Append("\n");
            sb.Append("  Longitude: ").Append(Longitude).Append("\n");
            sb.Append("  Radius: ").Append(Radius).Append("\n");
            sb.Append("  Duration: ").Append(Duration).Append("\n");
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
            return Equals((Truck)obj);
        }

        /// <summary>
        /// Returns true if Truck instances are equal
        /// </summary>
        /// <param name="other">Instance of Truck to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Truck other)
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
                    this.NumberPlate == other.NumberPlate ||
                    this.NumberPlate != null &&
                    this.NumberPlate.Equals(other.NumberPlate)
                ) && 
                (
                    this.Latitude == other.Latitude ||
                    this.Latitude != null &&
                    this.Latitude.Equals(other.Latitude)
                ) && 
                (
                    this.Longitude == other.Longitude ||
                    this.Longitude != null &&
                    this.Longitude.Equals(other.Longitude)
                ) && 
                (
                    this.Radius == other.Radius ||
                    this.Radius != null &&
                    this.Radius.Equals(other.Radius)
                ) && 
                (
                    this.Duration == other.Duration ||
                    this.Duration != null &&
                    this.Duration.Equals(other.Duration)
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
                    if (this.NumberPlate != null)
                    hash = hash * 59 + this.NumberPlate.GetHashCode();
                    if (this.Latitude != null)
                    hash = hash * 59 + this.Latitude.GetHashCode();
                    if (this.Longitude != null)
                    hash = hash * 59 + this.Longitude.GetHashCode();
                    if (this.Radius != null)
                    hash = hash * 59 + this.Radius.GetHashCode();
                    if (this.Duration != null)
                    hash = hash * 59 + this.Duration.GetHashCode();
                return hash;
            }
        }

        #region Operators

        public static bool operator ==(Truck left, Truck right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Truck left, Truck right)
        {
            return !Equals(left, right);
        }

        #endregion Operators

    }
}
