using SX.WebCore.Managers;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;

namespace vru.Controllers
{
    public sealed class QuestionsController : BaseController
    {
        private static RepoQuestions _repo;
        public QuestionsController()
        {
            if (_repo == null)
                _repo = new RepoQuestions();
        }

        [HttpGet]
        public ViewResult Edit()
        {
            var data = new Question();
            var viewModel = Mapper.Map<Question, VMQuestion>(data);
            return View(viewModel);
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

                var send= await sendMail(model);
                if(!send)
                {
                    ViewBag.Message = "Не удалось отправить сообщение";
                    return PartialView("_Add", model);
                }

                ViewBag.Message = "Ваше обращение успешно добавлено";
                return PartialView("_Add", new VMQuestion());
            }

            ViewBag.Message = "Что-то пошло не так. Обновите страницу и попробуйте еще раз";
            return PartialView("_Add", model);
        }

        private static async Task<bool> sendMail(VMQuestion model)
        {
            var smtpUserName = ConfigurationManager.AppSettings["NoReplyMail"];
            var mm = new SxAppMailManager(smtpUserName, ConfigurationManager.AppSettings["NoReplyMailPassword"], "mail.valliulina.ru");

            var sb = new StringBuilder();
            sb.AppendLine(model.Name);
            sb.AppendLine(model.Text);
            sb.AppendLine(model.Email);
            sb.AppendLine(model.Phone);

            return await mm.SendMail(model.Email, sb.ToString(), new string[] { "jdsl24221@ya.ru" }, "Обращение с формы обратной связи");
        }
    }
}