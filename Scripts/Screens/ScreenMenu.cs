using System;
using System.Collections.Generic;
using static SDL2.SDL;
using Clangen.UI;
using Clangen.Cats;

namespace Clangen.Screens
{
    public class Menu : BaseScreen
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
            ButtonClanSwitch = new Element(
                70, 355, 192, 35,
                ctx.GetTexture("Images\\Buttons\\switch_clan.png"),
                ctx.GetTexture("Images\\Buttons\\switch_clan_hover.png"),
                ctx.GetTexture("Images\\Buttons\\switch_clan_unavailable.png"),
                (GameState c) => c.SetScreen("SwitchClan")
            );
            ButtonClanNew = new Element(
                70, 400, 192, 35,
                ctx.GetTexture("Images\\Buttons\\new_clan.png"),
                ctx.GetTexture("Images\\Buttons\\new_clan_hover.png"),
                ctx.GetTexture("Images\\Buttons\\new_clan_unavailable.png"),
                (GameState c) => c.SetScreen("ClanCreation")
            );
            ButtonSettings = new Element(
                70, 445, 192, 35,
                ctx.GetTexture("Images\\Buttons\\settings.png"),
                ctx.GetTexture("Images\\Buttons\\settings_hover.png"),
                ctx.GetTexture("Images\\Buttons\\settings_unavailable.png"),
                (GameState c) => c.SetScreen(c.Screens["Settings"])
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

            ctx.CurrentClan = new Clan();

            ButtonContinue.Enabled = false;
            ButtonClanSwitch.Enabled = false;

            Elements = new List<BaseElement> { ButtonContinue, ButtonClanSwitch, ButtonClanNew, ButtonSettings, ButtonQuit, SocialTwitter, SocialTumblr, SocialDiscord };
        }


        public class SwitchClan : BaseScreen
        {
            public override void OnOpen(GameState ctx)
            {
                base.OnOpen(ctx);
            }
        }


        public class ClanCreation : BaseScreen
        {
            public BaseElement ButtonClassic;
            public BaseElement ButtonExpanded;
            public BaseElement ButtonCruel;

            public override void OnOpen(GameState ctx)
            {
                if (BaseTexture == IntPtr.Zero)
                    BaseTextureRect = new SDL_Rect { x = 0, y = 0, w = ctx.ScreenWidth, h = ctx.ScreenHeight };

                ButtonClassic = new Element(
                    109, 240, 132, 30,
                    ctx.GetTexture("Images\\Buttons\\classic_mode.png"),
                    ctx.GetTexture("Images\\Buttons\\classic_mode_hover.png"),
                    ctx.GetTexture("Images\\Buttons\\classic_mode_unavailable.png"),
                    (GameState c) =>
                    {
                        c.CurrentClan.Gamemode = GamemodeType.Classic;
                    }
                ) ;
                ButtonExpanded = new Element(
                    94, 320, 162, 34,
                    ctx.GetTexture("Images\\Buttons\\expanded_mode.png"),
                    ctx.GetTexture("Images\\Buttons\\expanded_mode_hover.png"),
                    ctx.GetTexture("Images\\Buttons\\expanded_mode_unavailable.png"),
                    (GameState c) =>
                    {
                        c.CurrentClan.Gamemode = GamemodeType.Expanded;
                    }
                );
                ButtonCruel = new Element(
                    100, 400, 150, 30,
                    ctx.GetTexture("Images\\Buttons\\cruel_season.png"),
                    ctx.GetTexture("Images\\Buttons\\cruel_season_hover.png"),
                    null
                );

                Elements = new List<BaseElement>() { ButtonClassic, ButtonExpanded, ButtonCruel };
            }
        }


        public class Settings : BaseScreen
        {
            public BaseElement ButtonSettings;
            public BaseElement ButtonSettingsSave;
            public BaseElement ButtonSettingsRelation;
            public BaseElement ButtonLanguage;
            public BaseElement ButtonInfo;
            public BaseElement ButtonMainMenu;

            public override void OnOpen(GameState ctx)
            {
                if (BaseTexture == IntPtr.Zero)
                    BaseTextureRect = new SDL_Rect { x = 0, y = 0, w = ctx.ScreenWidth, h = ctx.ScreenHeight };

                ButtonSettings = new Element(
                    100, 100, 150, 30,
                    ctx.GetTexture("Images\\Buttons\\general_settings.png"),
                    ctx.GetTexture("Images\\Buttons\\general_settings_hover.png"),
                    ctx.GetTexture("Images\\Buttons\\general_settings_unavailable.png")
                );
                ButtonSettingsSave = new Element(
                   327, 550, 146, 30,
                    ctx.GetTexture("Images\\Buttons\\save_settings.png"),
                    ctx.GetTexture("Images\\Buttons\\save_settings_hover.png"),
                    ctx.GetTexture("Images\\Buttons\\save_settings_unavailable.png")
                );
                ButtonSettingsRelation = new Element(
                    250, 100, 150, 30,
                    ctx.GetTexture("Images\\Buttons\\relation_settings.png"),
                    ctx.GetTexture("Images\\Buttons\\relation_settings_hover.png"),
                    ctx.GetTexture("Images\\Buttons\\relation_settings_unavailable.png")
                );
                ButtonLanguage = new Element(
                   550, 100, 150, 30,
                    ctx.GetTexture("Images\\Buttons\\language.png"),
                    ctx.GetTexture("Images\\Buttons\\language_hover.png"),
                    ctx.GetTexture("Images\\Buttons\\language_unavailable.png")
                );
                ButtonInfo = new Element(
                    400, 100, 150, 30,
                    ctx.GetTexture("Images\\Buttons\\info.png"),
                    ctx.GetTexture("Images\\Buttons\\info_hover.png"),
                    ctx.GetTexture("Images\\Buttons\\info_unavailable.png")
                );
                ButtonMainMenu = new Element(
                    25, 25, 150, 30,
                    ctx.GetTexture("Images\\Buttons\\main_menu.png"),
                    ctx.GetTexture("Images\\Buttons\\main_menu_hover.png"),
                    ctx.GetTexture("Images\\Buttons\\main_menu_unavailable.png"),
                    (GameState c) => c.SetScreen("Menu")
                );

                Elements = new List<BaseElement>() { ButtonSettings, ButtonSettingsSave, ButtonSettingsRelation, ButtonLanguage, ButtonInfo, ButtonMainMenu };
            }
        }
    }
}
