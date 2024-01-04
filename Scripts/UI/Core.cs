using Clangen;
using System;
using System.Collections.Generic;
using static SDL2.SDL;

namespace Clangen.UI
{
    public struct ElementTooltip
    {
        string Text;
    };

    public class BaseElement
    {
        public bool Enabled = true;
        public bool Hovered = false;
        public bool Visible = false;

        public virtual IntPtr Image { get; set; }
        public SDL_Rect Rect;

        protected BaseElement(int X, int Y, int W, int H, Action<GameState> Callback = null)
        {
            Rect = new SDL_Rect { x = X, y = Y, w = W, h = H };

            if (Callback != null )
                OnClick = Callback;
        }

        public Action<GameState> OnClick;
        public Action<GameState> OnClickEnd;
        public Action<GameState> OnDoubleClick;
        public Action<GameState> OnDoubleClickEnd;
        public Action<GameState> OnHover;
        public Action<GameState> OnHoverOff;

        public virtual void Build(GameState ctx) { }
        public virtual void Render(GameState ctx)
        {
            SDL_RenderCopy(ctx.Renderer, Image, IntPtr.Zero, ref Rect);
        }
    }


    public class Element : BaseElement
    {
        public IntPtr _Image;

        public override IntPtr Image {
            get => (Enabled ? Hovered ? ImageHovered : null : ImageUnavailable) ?? _Image;
            set => _Image = value;
            
        }
        public IntPtr? ImageHovered;
        public IntPtr? ImageUnavailable;

        public Element(int X, int Y, int W, int H, IntPtr Image, IntPtr? ImageHovered, IntPtr? ImageUnavailable, Action<GameState> Callback = null) : base(X, Y, W, H, Callback)
        {
            _Image = Image;
            this.ImageHovered = ImageHovered;
            this.ImageUnavailable = ImageUnavailable;

            OnHover = Render; 
            OnHoverOff = Render;
            OnClickEnd = Render;
        }
    }




    public class BaseScreen
    {
        public IntPtr BaseTexture;
        public SDL_Rect BaseTextureRect;
        public List<BaseElement> Elements;

        protected BaseScreen()
        {

        }
        protected BaseScreen(GameState ctx)
        {
            
        }
        ~BaseScreen()
        {
            SDL_DestroyTexture(BaseTexture);
        }

        public virtual void Rebuild(GameState ctx)
        {
            if (BaseTexture != null)
            {
                SDL_RenderCopy(ctx.Renderer, BaseTexture, ref BaseTextureRect, ref BaseTextureRect);
                SDL_SetRenderTarget(ctx.Renderer, BaseTexture);
            }
            else
            {
                SDL_SetRenderTarget(ctx.Renderer, IntPtr.Zero);
            }
            

            for (int i = 0; i < Elements.Count; i++)
            {
                var Element = Elements[i];
                Element.Build(ctx);
                SDL_RenderCopy(ctx.Renderer, Element.Image, IntPtr.Zero, ref Element.Rect);
            }

            SDL_SetRenderTarget(ctx.Renderer, IntPtr.Zero);
        }

        public virtual void OnOpen(GameState ctx) 
        {
            Elements = new List<BaseElement>();
        }


        public virtual void OnClose(GameState ctx) 
        { 

        }
        
        public virtual void OnClick(GameState ctx) {
        
        }
    }
}
