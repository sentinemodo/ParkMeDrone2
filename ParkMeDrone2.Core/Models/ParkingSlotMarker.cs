using System;

namespace ParkMeDrone2.Models
{
    public class ParkingSlotMarker
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double? HeadingDegrees { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Label { get; set; } = "Free slot";

        public string CreatedAtDisplay => CreatedAt.LocalDateTime.ToString("g");

        /// <summary>
        /// True when RelativeX and RelativeY are in [0, 1] (normalized coordinates).
        /// </summary>
        public bool IsValid => RelativeX >= 0 && RelativeX <= 1 && RelativeY >= 0 && RelativeY <= 1;
    }
}
