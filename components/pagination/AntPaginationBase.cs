﻿using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace AntBlazor
{
    public class AntPaginationBase : AntDomComponentBase
    {
        [Parameter] public EventCallback<int> PageSizeChange { get; set; }

        [Parameter] public EventCallback<int> PageIndexChange { get; set; }

        [Parameter] public RenderFragment<PaginationTotalContext> ShowTotal { get; set; }

        /// <summary>
        /// 'default' | 'small'
        /// </summary>
        [Parameter] public string Size { get; set; } = "default";

        [Parameter] public int[] PageSizeOptions { get; set; } = { 10, 20, 30, 40 };

        [Parameter] public RenderFragment<PaginationItemRenderContext> ItemRender { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public bool ShowSizeChanger { get; set; }

        [Parameter] public bool HideOnSinglePage { get; set; }

        [Parameter] public bool ShowQuickJumper { get; set; }

        [Parameter] public bool Simple { get; set; }

        [Parameter] public bool Responsive { get; set; }

        [Parameter] public int Total { get; set; } = 0;

        [Parameter] public int PageIndex { get; set; } = 1;

        [Parameter] public int PageSize { get; set; } = 10;

        protected bool _showPagination { get; set; } = true;

        private string _size { get; set; } = "default";

        private int _total { get; set; }

        protected void SetClass()
        {
            var clsPrefix = "ant-pagination";
            ClassMapper
                .Add(clsPrefix)
                .If($"{clsPrefix}-simple", () => Simple)
                .If($"{clsPrefix}-disabled", () => Disabled)
                .If($"{clsPrefix}-mini", () => !Simple && Size == "small")
                ;
        }

        protected override void OnInitialized()
        {
            SetClass();
        }
    }

    public class PaginationTotalContext
    {
        public int Implicit { get; set; }

        public int[] Range { get; set; }
    }

    public class PaginationItemRenderContext
    {
        /// <summary>
        ///  'page' | 'prev' | 'next' | 'prev_5' | 'next_5'
        /// </summary>
        public string Implicit { get; set; }

        public int Page { get; set; }
    }
}