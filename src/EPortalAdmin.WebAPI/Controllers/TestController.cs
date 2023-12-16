using EPortalAdmin.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPortalAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        //write test get / post with body here
        [HttpGet]
        public IActionResult Get()
        {
            var result = new List<object>()
            {
                { new { Id = 1, Name = "Test 1" } },
                { new { Id = 2, Name = "Test 2" } },
                { new { Id = 3, Name = "Test 3" } },
                { new { Id = 4, Name = "Test 4" } },
                { new { Id = 5, Name = "Test 5" } }
            };

            return Ok(result);
        }
        [HttpPost]
        public IActionResult Post(Test test)
        {
            if(test.Id == null)
                throw new NotFoundException("Id is null");

            if(test.Name.Length > 6)
                throw new BusinessException("Name length is greater than 6");

            return Ok();
        }

    }

    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
