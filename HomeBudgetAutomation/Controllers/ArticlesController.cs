using HomeBudgetAutomation.Data;
using HomeBudgetAutomation.Dtos.Article;
using HomeBudgetAutomation.Models;
using HomeBudgetAutomation.Repositories.Contract;
using HomeBudgetAutomation.ServiceResponder;
using HomeBudgetAutomation.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudgetAutomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService _service;

        public ArticlesController(IArticlesService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ArticleDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ArticleDto>> GetAll()
        {
            var articles = _service.GetAll();

            if (articles.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when getting all articles");
                return StatusCode(500, ModelState);
            }

            return Ok(articles.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ArticleDto> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id);
            }

            var article = _service.GetById(id);

            if (article.Message == ServiceMessageType.NotFound)
            {
                return NotFound();
            }

            return Ok(article.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ArticleDto> Create([FromBody] CreateArticleDto article)
        {
            if (article == null)
            {
                return BadRequest(ModelState);
            }

            if (article.Name.Trim() == "")
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newArticle = _service.Add(article);

            if (newArticle.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when adding article {article}");
                return StatusCode(500, ModelState);
            }


            return Ok(newArticle.Data); 
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ArticleDto> Update(int id, [FromBody] UpdateArticleDto article)
        {
            if (article == null)
            {
                return BadRequest(ModelState);
            }

            if (article.Name.Trim() == "")
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newArticle = _service.Update(id, article);

            if (newArticle.Message == ServiceMessageType.NotFound)
            {
                return NotFound();
            }

            if (newArticle.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when updating article {article}");
                return StatusCode(500, ModelState);
            }

            return Ok(newArticle.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ArticleDto> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id);
            }

            var article = _service.DeleteById(id);

            if (article.Message == ServiceMessageType.NotFound)
            {
                return NotFound();
            }

            if (article.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when getting all articles");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
