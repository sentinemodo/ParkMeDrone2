using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkMeDrone2.Models;

namespace ParkMeDrone2.Tests.Models
{
    [TestClass]
    public class ParkingSlotMarkerTests
    {
        /// <summary>
        /// Test-first: we want a new marker to have a non-empty display label.
        /// </summary>
        [TestMethod]
        public void NewMarker_HasNonEmptyDisplayLabel()
        {
            var marker = new ParkingSlotMarker();
            Assert.IsFalse(string.IsNullOrWhiteSpace(marker.Label), "New marker should have a non-empty Label.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(marker.CreatedAtDisplay), "CreatedAtDisplay should be non-empty.");
        }

        /// <summary>
        /// Test-first: we want a marker to report whether its position is valid (normalized 0–1).
        /// This test fails until IsValid is implemented on ParkingSlotMarker.
        /// </summary>
        [TestMethod]
        public void Marker_IsValid_WhenRelativeCoordinatesInZeroToOne()
        {
            var marker = new ParkingSlotMarker { RelativeX = 0.5, RelativeY = 0.5 };
            Assert.IsTrue(marker.IsValid, "Marker with 0.5, 0.5 should be valid.");
        }

        [TestMethod]
        public void Marker_IsInvalid_WhenRelativeCoordinatesOutOfRange()
        {
            var marker = new ParkingSlotMarker { RelativeX = 1.5, RelativeY = 0.5 };
            Assert.IsFalse(marker.IsValid, "Marker with X=1.5 should be invalid.");
        }
    }
}
