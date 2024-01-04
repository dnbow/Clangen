
using Clangen;
using ClangenNET.UI;
using System;
using System.Collections.Generic;
using static SDL2.SDL;

namespace ClangenNET.Screens
{
    public class StartScreen : BaseScreen
    {
        public BaseElement ButtonContinue;
        public BaseElement ButtonClanSwitch;
        public BaseElement ButtonClanNew;
        public BaseElement ButtonSettings;
        public BaseElement ButtonQuit;
        public BaseElement SocialTwitter;
        public BaseElement SocialTumblr;
        public BaseElement SocialDiscord;

        public override void OnOpen(GameState ctx)
        {
            if (BaseTexture == IntPtr.Zero)
                BaseTexture = ctx.GetTexture("Images\\menu.png");

            SDL_QueryTexture(BaseTexture, out uint _, out int _, out BaseTextureRect.w, out BaseTextureRect.h);

            ButtonContinue = new Element(
                70, 310, 192, 35,
                ctx.GetTexture("Images\\Buttons\\continue.png"),
                ctx.GetTexture("Images\\Buttons\\continue_hover.png"),
                ctx.GetTexture("Images\\Buttons\\continue_unavailable.png")
            );
            ButtonContinue.Enabled = false;
            ButtonClanSwitch = new Element(
                70, 355, 192, 35,
                ctx.GetTexture("Images\\Buttons\\switch_clan.png"),
                ctx.GetTexture("Images\\Buttons\\switch_clan_hover.png"),
                ctx.GetTexture("Images\\Buttons\\switch_clan_unavailable.png")
            );
            ButtonClanSwitch.Enabled = false;
            ButtonClanNew = new Element(
                70, 400, 192, 35,
                ctx.GetTexture("Images\\Buttons\\new_clan.png"),
                ctx.GetTexture("Images\\Buttons\\new_clan_hover.png"),
                ctx.GetTexture("Images\\Buttons\\new_clan_unavailable.png")
            );
            ButtonSettings = new Element(
                70, 445, 192, 35,
                ctx.GetTexture("Images\\Buttons\\settings.png"),
                ctx.GetTexture("Images\\Buttons\\settings_hover.png"),
                ctx.GetTexture("Images\\Buttons\\settings_unavailable.png")
            );
            ButtonQuit = new Element(
                70, 490, 192, 35,
                ctx.GetTexture("Images\\Buttons\\quit.png"),
                ctx.GetTexture("Images\\Buttons\\quit_hover.png"),
                ctx.GetTexture("Images\\Buttons\\quit_unavailable.png"),
                (GameState c) => c.Quit()
            );
            SocialTwitter = new Element(
                15, 650, 40, 40,
                ctx.GetTexture("Images\\Buttons\\twitter.png"),
                ctx.GetTexture("Images\\Buttons\\twitter_hover.png"),
                ctx.GetTexture("Images\\Buttons\\twitter_unavailable.png"),
                (GameState _) => SDL_OpenURL("https://twitter.com/OfficialClangen/")
            );
            SocialTumblr = new Element(
                60, 650, 40, 40,
                ctx.GetTexture("Images\\Buttons\\tumblr.png"),
                ctx.GetTexture("Images\\Buttons\\tumblr_hover.png"),
                ctx.GetTexture("Images\\Buttons\\tumblr_unavailable.png"),
                (GameState _) => SDL_OpenURL("https://officialclangen.tumblr.com/")
            );
            SocialDiscord = new Element(
                105, 650, 40, 40,
                ctx.GetTexture("Images\\Buttons\\discord.png"),
                ctx.GetTexture("Images\\Buttons\\discord_hover.png"),
                ctx.GetTexture("Images\\Buttons\\discord_unavailable.png"),
                (GameState _) => SDL_OpenURL("https://discord.com/invite/clangen")
            );

            Elements = new List<BaseElement> { ButtonContinue, ButtonClanSwitch, ButtonClanNew, ButtonSettings, ButtonQuit, SocialTwitter, SocialTumblr, SocialDiscord };
        }



        public override void OnClose(GameState ctx)
        {
            Elements.Clear();
        }


        public override void Rebuild(GameState ctx)
        {
            SDL_RenderCopy(ctx.Renderer, BaseTexture, ref BaseTextureRect, ref BaseTextureRect);
            SDL_SetRenderTarget(ctx.Renderer, BaseTexture);

            for (int i = 0; i < Elements.Count; i++)
            {
                var Element = Elements[i];
                Element.Build(ctx);
                SDL_RenderCopy(ctx.Renderer, Element.Image, IntPtr.Zero, ref Element.Rect);
            }

            SDL_SetRenderTarget(ctx.Renderer, BaseTexture);
            
        }
    }
}
