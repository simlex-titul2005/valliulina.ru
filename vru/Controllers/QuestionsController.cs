using System.Threading.Tasks;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;

namespace vru.Controllers
{
    public class QuestionsController : BaseController
    {
        private static RepoQuestions _repo;
        public QuestionsController()
        {
            if (_repo == null)
                _repo = new RepoQuestions();
        }

        [ChildActionOnly]
        public PartialViewResult Add()
        {
            var data = new Question();
            var viewModel = Mapper.Map<Question, VMQuestion>(data);
            return PartialView("_Add", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<PartialViewResult> Send(VMQuestion model)
        {
            if (ModelState.IsValid)
            {
                var redactModel = Mapper.Map<VMQuestion, Question>(model);
                var addedModel = await Task.Run(() => _repo.Create(redactModel));
                if (addedModel == null)
                {
                    ViewBag.Message = "Что-то пошло не так. Попробуйте позднее";
                    return PartialView("_Add", model);
                }

                ViewBag.Message = "Ваше обращение успешно добавлено";
                return PartialView("_Add", new VMQuestion());
            }

            ViewBag.Message = "Что-то пошло не так. Обновите страницу и попробуйте еще раз";
            return PartialView("_Add", model);
        }
    }
}