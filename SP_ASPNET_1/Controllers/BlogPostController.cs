using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;
using System.Web.Mvc;
using System.Web.Routing;
using SP_ASPNET_1.BusinessLogic;
using System;

namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("Blog")]
    public class BlogPostController : Controller
    {
        private readonly BlogPostOperations _blogPostOperations = new BlogPostOperations();
      
       // [OutputCache(CacheProfile = "Cache5Min")]
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            //return this.View();
            BlogIndexViewModel result = this._blogPostOperations.GetBlogIndexViewModel();
            ViewBag.Title = "Blog";
            return this.View(result);
        }


        [Route("Detail/{id:int?}")]
        [HttpGet]
        public ActionResult SinglePost(int? id)
        {
            ViewBag.Title = "single post";

            
            BlogSinglePostViewModel modelView;

            if (id == null)
            {
                modelView = this._blogPostOperations.GetLatestBlogPost();
            }
            else
            {
                modelView = this._blogPostOperations.GetBlogPostByIdFull((int)id);
            }
            ViewData ["AvgLike"] = this._blogPostOperations.GetAuthorAvgLike(((int)id));

            return View(modelView);
        }

        [Route("Detail/Random")]
        [HttpGet]
        public ActionResult RandomPost()
        {
            ViewBag.Title = "Random post";

            var viewModel = this._blogPostOperations.GetRandomBlogPost();

            return View(viewModel);
        }

        [Route("LatestPost")]
        [HttpGet]
        public ActionResult LatestPost()
        {
            var viewModel = this._blogPostOperations.GetLatestBlogPost();

            return this.PartialView("~/Views/BlogPost/_BlogPostRecentPartialView.cshtml", viewModel);
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(BlogPost blogPost)
        {
            try
            {
                this._blogPostOperations.Create(blogPost);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [Route("Edit/{id:int?}")]
        [HttpGet]
        public ActionResult EditBlogPost(int id)
        {
            BlogPost blogPost;

            blogPost = this._blogPostOperations.GetBlogPostByIdD((int)id);

            return View(blogPost);
        }


        [Route("Edit/{id:int}")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                // TODO: Return to detail
                BlogPost blogpost;

                blogpost = this._blogPostOperations.GetBlogPostByIdD((int)id);
                return View(blogpost);

              //  throw new NotImplementedException("Editing blog post is not implemented");
            }
            catch
            {
                return View();
            }
        }

        [Route("EditBlog")]
        [HttpPost]
        public ActionResult EditBlog(BlogPost editedBlogPost)
        {
            try
            {
                // TODO: Add update logic here
                // TODO: Return to detail
                BlogPost blogpost;

                blogpost = this._blogPostOperations.GetBlogPostByIdD((int)editedBlogPost.BlogPostID);
                if (blogpost != null)
                {
                    blogpost.Title = editedBlogPost.Title;
                    blogpost.DateTime = editedBlogPost.DateTime;
                    blogpost.Content = editedBlogPost.Content;
                    blogpost.ImageUrl = editedBlogPost.ImageUrl;
                    blogpost.LikeQty = editedBlogPost.LikeQty;

                    this._blogPostOperations.Update(blogpost);

                    return Json(new { success = true, responseText = "Blog Sucessfully edited!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Blog update failed!" }, JsonRequestBehavior.AllowGet);
                }

               //return View(blogpost);


              //throw new NotImplementedException("Editing blog post is not implemented");
            }
            catch
            {
                return Json(new { success = false, responseText = "Blog update failed!" }, JsonRequestBehavior.AllowGet);
                //   return View();
            }
        }
      
        [Route("Delete/{id:int}")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                this._blogPostOperations.Delete(id);

                //CHECK: should return to blogs
                return RedirectToAction("Index");
            }
            catch
            {
                return this.HttpNotFound();
            }
        }
    }
}
