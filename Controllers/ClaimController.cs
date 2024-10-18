// Controllers/ClaimController.cs
using Microsoft.AspNetCore.Mvc;
using ST10384480CMCS.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ST10384480CMCS.Controllers
{
    public class ClaimController : Controller
    {
        private static List<Claim> claims = new List<Claim>();

        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.ClaimId = claims.Count + 1;
                claim.SubmissionDate = DateTime.Now;
                claim.Status = "Pending";
                claims.Add(claim);
                return RedirectToAction("Privacy");
            }
            return View(claim);
        }

        public IActionResult Privacy()
        {
            return View(claims);
        }

        [HttpPost]
        public IActionResult Approve(int claimId)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                claim.Status = "Approved";
            }
            return RedirectToAction("Privacy");
        }

        [HttpPost]
        public IActionResult Reject(int claimId)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == claimId);
            if (claim != null)
            {
                claim.Status = "Rejected";
            }
            return RedirectToAction("Privacy");
        }
    }
}