using Clangen;
using System;
using System.Collections.Generic;
using System.Linq;
using static SDL2.SDL;

namespace ClangenNET.UI
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
        public Action<GameState> OnDoubleClick;
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
            get => Hovered ? ImageHovered ?? Image : (!Enabled ? ImageUnavailable ?? Image : _Image);
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
        protected BaseScreen(IntPtr Texture, SDL_Rect TextureRect, BaseElement[] ElementArray)
        {
            BaseTexture = Texture;
            BaseTextureRect = TextureRect;
            Elements = ElementArray.ToList();
        }
        ~BaseScreen()
        {
            SDL_DestroyTexture(BaseTexture);
            Elements.Clear();
        }


        public virtual void Rebuild(GameState context) { }
        public virtual void OnOpen(GameState context) { }
        public virtual void OnClose(GameState context) { }
        public virtual void OnClick(GameState context) { }
    }
}
