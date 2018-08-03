using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        private HtmlIframe holder;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                foreach (var Pageitem in this.Controls)
                {

                    if (Pageitem.GetType() == typeof(HtmlForm))
                    {

                        foreach (var HtmlFormitem in ((HtmlForm)Pageitem).Controls)
                        {
                            //if (HtmlFormitem.GetType() == typeof(HtmlIframe))
                            //{
                            //    ((HtmlIframe)HtmlFormitem).Src = "home/index";
                            //}


                            if (HtmlFormitem.GetType() == typeof(ContentPlaceHolder))
                            {
                                foreach (var ContentPlaceHolderitem in ((ContentPlaceHolder)HtmlFormitem).Controls)
                                {
                                    if (ContentPlaceHolderitem.GetType() == typeof(HtmlIframe))
                                    {
                                        holder = (HtmlIframe)ContentPlaceHolderitem;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        protected void ButtonAdminIndex_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonSiteBookingHestory_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonAdminProfile_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButtonGeustBookingHestory_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Geust,Detailes,null,new{id =" + Session["UserID"] + "}";
        }

        protected void ImageButtonGeustProfile_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Creat,Places";
        }

        protected void ImageButtonHostBoockingHestory_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Hosts,Detailes,null,new{id =" + Session["UserID"] + "}";
        }

        protected void ImageButtonHostProfile_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Hosts,Detailes";
        }

        protected void ButtonAdminProfile_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Users,Detailes";
        }

        protected void ImageButtonGestInbox_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Index,Index";
        }

        protected void ImageButtonAddPlace_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Index,Index";
        }

        protected void ButtonAdminIndex_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Index,Index";
        }

        protected void ImageButtonGeustProfile_Click1(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Geusts,Detailes";
        }

        protected void ButtonSiteBookingHestory_Click(object sender, ImageClickEventArgs e)
        {
            holder.Src = "Users,Detailes,null,new{id =" + Session["UserID"] + "}";
        }
    }
}