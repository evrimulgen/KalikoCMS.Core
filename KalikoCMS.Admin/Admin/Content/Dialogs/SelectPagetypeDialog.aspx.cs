﻿#region License and copyright notice
/* 
 * Kaliko Content Management System
 * 
 * Copyright (c) Fredrik Schultz
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 * http://www.gnu.org/licenses/lgpl-3.0.html
 */
#endregion

namespace KalikoCMS.Admin.Content.Dialogs {
    using System;
    using System.Linq;
    using System.Text;
    using KalikoCMS.Core;

    public partial class SelectPagetypeDialog : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            var pageTypes = PageType.PageTypes;

            if (pageTypes == null) {
                return;
            }

            var pageId = new Guid(Request.QueryString["pageId"]);
            var parent = PageFactory.GetPage(pageId);
            var parentPageType = pageTypes.Find(p => p.PageTypeId == parent.PageTypeId);
            var allowAll = parentPageType.AllowedTypes == null;

            if (parentPageType.AllowedTypes != null && parentPageType.AllowedTypes.Length == 0) {
                PageTypeList.Text = "No pages can be created under the selected page.";
                return;
            }
            
            var stringBuilder = new StringBuilder();

            foreach (var pageType in pageTypes) {
                if (allowAll || parentPageType.AllowedTypes.Contains(pageType.Type)) {
                    stringBuilder.Append("<dt><a href=\"javascript:selectPageType('" + pageType.PageTypeId + "')\">" + pageType.DisplayName + "</a></dt><dd>" + pageType.PageTypeDescription + "</dd>");
                }
            }

            PageTypeList.Text = stringBuilder.ToString();
        }
    }
}