using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UniversityInfo.Service;
using UniversityInfo.Models;
using UniversityInfo.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Query;

namespace UniversityInfo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController: ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly UserManager<User> _userManager;
        public UniversityController(DataContext dataContext, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext;
        }

        [HttpGet]
        [EnableQuery(EnsureStableOrdering = false)]
        public IActionResult GetUniversities()
        {
            try
            {
                return Ok(_dataContext.Universities.OrderByDescending(x=> x.IsBookMarked).AsQueryable());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUniversity(Guid id)
        {
            try
            {
                var university = _dataContext.Universities.FirstOrDefault(x=> x.Id == id);
                if(university!= null)
                    return Ok(university);
                else return NotFound("University Id not found");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUniversity([FromBody]University university)
        {
            university.Created = DateTime.Now;
            var novel = _dataContext.Universities.Add(university);
            
            await _dataContext.SaveChangesAsync();
            
            return Ok($"University information created.");

        }

        [HttpPost("bookmark/{id}")]
        public async Task<IActionResult> BookmarkUniversity(Guid id)
        {
            var university = _dataContext.Universities.Find(id);
            if (university == null) return NotFound("University Id not found");

            university.IsBookMarked = !university.IsBookMarked;
            await _dataContext.SaveChangesAsync();
            return Ok(university.IsBookMarked? "University information bookmarked.": "Removed bookmark from university info");

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUniversity(Guid id, [FromBody]University university)
        {
            var u = _dataContext.Universities.Find(id);
            if (u == null) return NotFound("University Id not found");

            university.LastModified = DateTime.Now;
            _dataContext.Entry(u).CurrentValues.SetValues(university);
            await _dataContext.SaveChangesAsync();
            return Ok($"University information updated.");

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveUniversity(Guid id)
        {
            var university = _dataContext.Universities.FirstOrDefault(x=> x.Id == id);
            if(university == null) return NotFound("University Id not found");

            _dataContext.Universities.Remove(university);
            await _dataContext.SaveChangesAsync();
            return Ok($"University info removed!");

        }
    }

}