// ST10384480CMCS.Tests/ClaimControllerTests.cs
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ST10384480CMCS.Controllers;
using ST10384480CMCS.Models;
using System.Collections.Generic;

namespace ST10384480CMCS.Tests
{
    public class ClaimControllerTests
    {
        // Test for GET: SubmitClaim action
        [Fact]
        public void SubmitClaim_ReturnsViewResult_WhenCalled()
        {
            // Arrange
            var controller = new ClaimController();

            // Act
            var result = controller.SubmitClaim();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        // Test for POST: SubmitClaim action with a valid model
        [Fact]
        public void SubmitClaim_RedirectsToPrivacy_WhenModelIsValid()
        {
            // Arrange
            var controller = new ClaimController();
            var claim = new Claim
            {
                LecturerId = "L123",
                HoursWorked = 5,
                Rate = 100,
                AdditionalNotes = "Sample note"
            };

            // Act
            var result = controller.SubmitClaim(claim);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Privacy", redirectToActionResult.ActionName);
        }

        // Test for POST: SubmitClaim action with an invalid model
        [Fact]
        public void SubmitClaim_ReturnsViewResult_WhenModelIsInvalid()
        {
            // Arrange
            var controller = new ClaimController();
            var claim = new Claim(); // Missing required fields to make model invalid
            controller.ModelState.AddModelError("LecturerId", "Required");

            // Act
            var result = controller.SubmitClaim(claim);

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        // Test for GET: Privacy action
        [Fact]
        public void Privacy_ReturnsViewWithClaims_WhenCalled()
        {
            // Arrange
            var controller = new ClaimController();
            // Clear claims to ensure no state leak
            controller.SubmitClaim(new Claim
            {
                LecturerId = "L123",
                HoursWorked = 5,
                Rate = 100,
                AdditionalNotes = "Sample note"
            });

            // Act
            var result = controller.Privacy();

            // Cast the result to ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);

            // Access the Model property after casting
            var model = Assert.IsAssignableFrom<List<Claim>>(viewResult.Model);

            // Assert
            Assert.Single(model); // This will pass if only one claim is added
        }

        // Test for POST: Approve action
        [Fact]
        public void Approve_ChangesClaimStatusToApproved_WhenCalled()
        {
            // Arrange
            var controller = new ClaimController();
            var claim = new Claim
            {
                LecturerId = "L123",
                HoursWorked = 5,
                Rate = 100,
                AdditionalNotes = "Sample note"
            };
            controller.SubmitClaim(claim); // Adding claim before approving

            // Act
            var result = controller.Approve(claim.ClaimId);

            // Assert that the result is a RedirectToActionResult
            Assert.IsType<RedirectToActionResult>(result);

            // Call the Privacy action again to verify changes
            var privacyResult = controller.Privacy();

            // Cast the result to ViewResult
            var viewResult = Assert.IsType<ViewResult>(privacyResult);

            // Access the Model property after casting
            var model = Assert.IsAssignableFrom<List<Claim>>(viewResult.Model);
            Assert.Equal("Approved", model[0].Status);
        }

        // Test for POST: Reject action
        [Fact]
        public void Reject_ChangesClaimStatusToRejected_WhenCalled()
        {
            // Arrange
            var controller = new ClaimController();
            var claim = new Claim
            {
                LecturerId = "L123",
                HoursWorked = 5,
                Rate = 100,
                AdditionalNotes = "Sample note"
            };
            controller.SubmitClaim(claim); // Adding claim before rejecting

            // Act
            var result = controller.Reject(claim.ClaimId);

            // Assert that the result is a RedirectToActionResult
            Assert.IsType<RedirectToActionResult>(result);

            // Call the Privacy action again to verify changes
            var privacyResult = controller.Privacy();

            // Cast the result to ViewResult
            var viewResult = Assert.IsType<ViewResult>(privacyResult);

            // Access the Model property after casting
            var model = Assert.IsAssignableFrom<List<Claim>>(viewResult.Model);
            Assert.Equal("Rejected", model[0].Status);
        }
    }
}