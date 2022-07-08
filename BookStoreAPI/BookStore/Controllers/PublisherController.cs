using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("publisher")]
    public class PublisherController : ControllerBase
    {
        PublisherRepository _publisherRepository=new PublisherRepository();
        [HttpGet]
        [Route("getPublishers")]
        [ProducesResponseType(typeof(ListResponse<PublisherModel>),(int)HttpStatusCode.OK)] 
        public IActionResult getPublishers(string keyword,int pageSize=10,int pageIndex=1)
        {
            var query = _publisherRepository.GetPublishers(pageSize,pageIndex,keyword);
            ListResponse<PublisherModel> response = new ListResponse<PublisherModel>()
            {
                Results = query.Results.Select(c => new PublisherModel(c)),
                TotalRecords = query.TotalRecords,
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("getPublisher")]
        [ProducesResponseType(typeof(PublisherModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public IActionResult getPublisher(int id)
        {
            var query = _publisherRepository.GetPublisher(id);
            if (query == null)
            {
                return NotFound();
            }
            PublisherModel response = new PublisherModel(query);
            return Ok(response);
        }

        [HttpPost]
        [Route("addPublisher")]
        [ProducesResponseType(typeof(PublisherModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult addPublishers(PublisherModel model)
        {
            if (model==null)
            {
                return BadRequest();
            }
            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact
            };
            var query=_publisherRepository.AddPublisher(publisher);
            PublisherModel publisherModel = new PublisherModel(query);
            return Ok(publisherModel);
        }

        [HttpPut]
        [Route("updatePublisher")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult updatePublisher(PublisherModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact
            };
            var query=_publisherRepository.UpdatePublisher(publisher);
            PublisherModel publisherModel=new PublisherModel(query);

            return Ok(publisherModel);
        }

        [HttpDelete]
        [Route("deletePublisher")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult deletePublisher(int id)
        {
            if (id == 0)
                return BadRequest("Id is null");

            var query=_publisherRepository.DeletePublisher(id);
            return Ok(query);
        }
    }
}
