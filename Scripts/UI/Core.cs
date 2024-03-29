﻿using System;
using System.Collections.Generic;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static Clangen.Utility;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace Clangen.UI
{
    public enum MouseButton
    {
        Left, Middle, Right
    };



    public delegate void UIEvent(object sender, EventArgs e);
    public delegate void UIMouseEvent(MouseButton button, bool isLifted);


    public abstract class BaseElement
    {
        protected bool _Enabled, _Hovered, _Visible;

        public bool Enabled {
            get => _Enabled;
            set
            {
                if (value)
                    Enable();
                else 
                    Disable();
            }
        }

        public bool Hovered
        {
            get => _Hovered;
            set
            {
                _Hovered = value;
                Render();
            }
        }

        public bool Visible
        {
            get => _Visible;
            set {
                _Visible = value;
                Render();
            }
        }

        public virtual void Enable()
        {
            _Enabled = true;
            Render();
        }

        public virtual void Disable()
        {
            _Enabled = false;
            _Hovered = false;
            Render();
        }



        public virtual Image Texture { get; set; }
        public SDL_Rect Rect;

        public event UIEvent OnClick;
        public event UIEvent OnHover;
        public event UIEvent OnHoverOff;

        protected BaseElement(int X, int Y, int W, int H)
        {
            Rect = new SDL_Rect { x = X, y = Y, w = W, h = H };
        }

        public void ClickEvent()
        {
            OnClick?.Invoke(this, EventArgs.Empty);
        }

        public void HoverEvent(bool State)
        {
            (State ? OnHover : OnHoverOff)?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Render()
        {
            Context.Render(Texture, null, Rect);
        }
    }

    public class Element : BaseElement
    {
        private Image _ImageNormal;
        private Image _ImageHover;
        private Image _ImageDisable;

        public Element(
            int X, int Y, int W, int H, bool Enabled, bool Visible, Image Normal, Image Hover = null, Image Unavailable = null, UIEvent Callback = null
        ) : base(X, Y, W, H)
        {
            _Enabled = Enabled;
            _Visible = Visible;
            _Hovered = false;
            
            _ImageNormal = Normal;
            _ImageHover = Hover;
            _ImageDisable = Unavailable;

            if (!(Callback is null))
            {
                OnClick += Callback;
            }

            OnHover += (_, __) => { 
                if (Enabled) 
                    Context.Render(_ImageHover ?? _ImageNormal, null, Rect);

            };
            OnHoverOff += (_, __) =>
            {
                Context.Render(_ImageNormal, null, Rect);
            };

            Texture = Enabled ? _ImageNormal : _ImageDisable;
        }
        public Element(int X, int Y, int W, int H, Image Normal, Image Hover = null, Image Unavailable = null, UIEvent Callback = null) : this(X, Y, W, H, true, true, Normal, Hover, Unavailable, Callback)
        {

        }

        public override void Render()
        {
            Texture = (Enabled ? Hovered ? _ImageHover : null : _ImageDisable) ?? _ImageNormal;
            base.Render();
        }
    }

    public class RawTextbox : BaseElement
    {
        private string _Text;
        public Font FontType;
        public Color FontColor;

        //private Image RenderedText;

        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                Build();
                Render();
            }
        }

        public RawTextbox(int X, int Y, int W, int H, string DefaultText, string Font, Color? Color = null) : base(X, Y, W, H)
        {
            Rect = new SDL_Rect { x = X, y = Y, w = W, h = H };
            FontType = Context.Fonts[Font];
            FontColor = Color ?? Context.Theme.FontColor;
            _Text = DefaultText;

            Build();
        }

        public void Build()
        {
            Texture = FontType.Render(Text, Rect);
        }

        public override void Render()
        {
            SDL_RenderFillRect(Context.Renderer, ref Rect);
            base.Render();
        }
    }



    public interface IScreen
    {
        Image Texture { get; }
        void Construct();
        void Render();
        void Deconstruct();
        void Tick(SDL_Event Event);
    }


    public class Screen : IScreen
    {
        public Image Background;
        public Image Texture { 
            get => _Texture;
            set => _Texture = value;
        }

        private Image _Texture;

        protected Dictionary<string, int> Lookup;
        protected List<BaseElement> Elements;
        protected List<BaseElement> HoveredElements;

        public Screen()
        {

        }
        ~Screen()
        {

        }

        public BaseElement this[string Key]
        {
            get
            {
                return Elements[Lookup[Key]];
            }
            set
            {
                if (Lookup.ContainsKey(Key))
                {
                    Elements[Lookup[Key]] = value;
                }
                else
                {
                    Lookup[Key] = Elements.Count;
                    Elements.Add(value);
                }
            }
        }

        public void Construct()
        {
            Lookup = new Dictionary<string, int>();
            Elements = new List<BaseElement>();
            HoveredElements = new List<BaseElement>();

            OnOpen();
        }

        public void Render()
        {
            if (_Texture == null)
                _Texture = new Image(800, 700, Context.Theme.Background);

            using (var Texture = new RenderTarget(_Texture))
            {

                if (Background is not null)
                    Context.Render(Background, null, Background.Rect);

                for (int i = 0; i < Elements.Count; i++)
                {
                    var element = Elements[i];
                    if (element.Visible)
                    {
                        element.Render();
                    }
                }
            }
        }

        public void Deconstruct()
        {
            if (!(Background is null))
                Background.Destroy();

            OnClose();
        }

        public void Tick(SDL_Event Event)
        {
            switch (Event.type)
            {
                case SDL_EventType.SDL_MOUSEMOTION:
                    for (int i = 0; i < Elements.Count; i++)
                    {
                        var element = Elements[i];
                        SDL_GetMouseState(out int X, out int Y);

                        if (element.Enabled && element.Visible)
                        {
                            SDL_Rect Rect = element.Rect;
                            bool isHovered = Rect.x <= X && X <= Rect.x + Rect.w && Rect.y <= Y && Y <= Rect.y + Rect.h;

                            if (isHovered && !element.Hovered)
                            {
                                element.Hovered = true;
                                element.HoverEvent(true);
                                HoveredElements.Add(element);
                            }

                            else if (!isHovered && element.Hovered)
                            {
                                element.Hovered = false;
                                element.HoverEvent(false);

                                HoveredElements.Remove(element);
                            }
                        }
                    }

                    break;

                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    for (int i = 0; i < HoveredElements.Count; i++) {
                        var element = HoveredElements[i];
                        if (element.Enabled)
                            HoveredElements[i].ClickEvent();
                    }
                    break;

                case SDL_EventType.SDL_MOUSEBUTTONUP:

                    break;

                default:
                    break;
            }
        }


        public virtual void OnOpen()
        {
            
        }

        public virtual void OnClose()
        {

        }
    }
}
