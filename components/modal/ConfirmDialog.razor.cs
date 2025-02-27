﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace AntDesign
{
    public partial class ConfirmDialog
    {
        [Parameter]
        public ConfirmOptions Config { get; set; }

        [Parameter]
        public EventCallback<ConfirmOptions> OnRemove { get; set; }

        private Button _cancelBtn;
        private Button _okBtn;

        private DialogOptions BuildDialogOptions(ConfirmOptions confirmOptions)
        {
            DialogOptions config = new DialogOptions()
            {
                Title = confirmOptions.Title,
                OkButtonProps = confirmOptions.OkButtonProps,
                CancelButtonProps = confirmOptions.CancelButtonProps,
                Width = confirmOptions.Width,
                Centered = confirmOptions.Centered,
                Mask = confirmOptions.Mask,
                MaskClosable = confirmOptions.MaskClosable,
                MaskStyle = confirmOptions.MaskStyle,
                OkText = confirmOptions.OkText,
                OkType = confirmOptions.OkType,
                CancelText = confirmOptions.CancelText,
                CloseIcon = confirmOptions.Icon,
                ZIndex = confirmOptions.ZIndex,
                Keyboard = confirmOptions.Keyboard,
                GetContainer = confirmOptions.GetContainer,
                Footer = null,
                TransitionName = confirmOptions.TransitionName,
                MaskTransitionName = confirmOptions.MaskTransitionName,

                ClassName = confirmOptions.ClassName,
            };

            config.ClassName = "ant-modal-confirm ant-modal-confirm-" + confirmOptions.ConfirmType;
            config.DestroyOnClose = true;
            config.Width = 416;
            config.Title = null;
            config.CloseIcon = null;
            config.OnClosed = Close;
            config.OnCancel = HandleCancel;
            return config;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Config.Visible && Config.AutoFocusButton != ConfirmAutoFocusButton.Null)
            {
                var element = Config.AutoFocusButton == ConfirmAutoFocusButton.Cancel
                    ? _cancelBtn
                    : _okBtn;
                await JsInvokeAsync(JSInteropConstants.focusDialog, $"#{element.Id}");
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task Close()
        {
            Config.Visible = false;
            await InvokeAsync(StateHasChanged);
            if (OnRemove.HasDelegate)
            {
                await OnRemove.InvokeAsync(Config);
            }
        }

        private async Task HandleOk(MouseEventArgs e)
        {
            if (Config.OnOk != null)
            {
                Config.OkButtonProps.Loading = true;
                await InvokeAsync(StateHasChanged);
                await Config.OnOk.Invoke(e);
            }
            await Close();
        }

        private async Task HandleCancel(MouseEventArgs e)
        {
            if (Config.OnCancel != null)
            {
                Config.CancelButtonProps.Loading = true;
                await InvokeAsync(StateHasChanged);
                await Config.OnCancel.Invoke(e);
            }
            await Close();
        }
    }
}
