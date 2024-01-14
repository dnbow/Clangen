using System;
using System.Collections.Generic;
using static SDL2.SDL;
using Clangen.UI;
using Clangen.Cats;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Clangen.Screens
{
    public class Menu : Screen
    {
        public override void OnOpen()
        {
            Background = Context.Images["menu.png"];

            this["ButtonContinue"] = new Element(
                70, 310, 192, 35,
                Context.Images["Buttons\\continue.png"],
                Context.Images["Buttons\\continue_hover.png"],
                Context.Images["Buttons\\continue_unavailable.png"]
            );
            this["ButtonClanSwitch"] = new Element(
                70, 355, 192, 35,
                Context.Images["Buttons\\switch_clan.png"],
                Context.Images["Buttons\\switch_clan_hover.png"],
                Context.Images["Buttons\\switch_clan_unavailable.png"],
                (e, s) => Context.Screens.SetScreen("SwitchClan")
            );
            this["ButtonClanNew"] = new Element(
                70, 400, 192, 35,
                Context.Images["Buttons\\new_clan.png"],
                Context.Images["Buttons\\new_clan_hover.png"],
                Context.Images["Buttons\\new_clan_unavailable.png"],
                (e, s) => Context.Screens.SetScreen("ClanCreation")
            );
            this["ButtonSettings"] = new Element(
                70, 445, 192, 35,
                Context.Images["Buttons\\settings.png"],
                Context.Images["Buttons\\settings_hover.png"],
                Context.Images["Buttons\\settings_unavailable.png"],
                (e, s) => Context.Screens.SetScreen("Settings")
            );
            this["ButtonQuit"] = new Element(
                70, 490, 192, 35,
                Context.Images["Buttons\\quit.png"],
                Context.Images["Buttons\\quit_hover.png"],
                Context.Images["Buttons\\quit_unavailable.png"],
                (e, s) => InternalContext.Quit()
            );
            this["SocialTwitter"] = new Element(
                15, 650, 40, 40,
                Context.Images["Buttons\\twitter.png"],
                Context.Images["Buttons\\twitter_hover.png"],
                Context.Images["Buttons\\twitter_unavailable.png"],
                (e, s) => SDL_OpenURL("https://twitter.com/OfficialClangen/")
            );
            this["SocialTumblr"] = new Element(
                60, 650, 40, 40,
                Context.Images["Buttons\\tumblr.png"],
                Context.Images["Buttons\\tumblr_hover.png"],
                Context.Images["Buttons\\tumblr_unavailable.png"],
                (e, s) => SDL_OpenURL("https://officialclangen.tumblr.com/")
            );
            this["SocialDiscord"] = new Element(
                105, 650, 40, 40,
                Context.Images["Buttons\\discord.png"],
                Context.Images["Buttons\\discord_hover.png"],
                Context.Images["Buttons\\discord_unavailable.png"],
                (e, s) => SDL_OpenURL("https://discord.com/invite/clangen")
            );

            Context.Clan = new Clan();

            this["ButtonContinue"].Enabled = false;
            this["ButtonClanSwitch"].Enabled = false;
        }
    }


    public class SwitchClan : Screen
    {
        public override void OnOpen()
        {

        }
    }


    public class ClanCreation : Screen
    {
        public byte Step = 0;

        public override void OnOpen()
        {
            this["ModeText"] = new RawTextbox(325, 150, 410, 461, "", "Clangen");

            RawTextbox ModeText = (RawTextbox)this["ModeText"];

            this["Classic"] = new Element(
                109, 240, 132, 30,
                Context.Images["Buttons\\classic_mode.png"],
                Context.Images["Buttons\\classic_mode_hover.png"],
                Context.Images["Buttons\\classic_mode_unavailable.png"],
                (s, e) =>
                {
                    Context.Clan.Gamemode = GamemodeType.Classic;
                    this["Classic"].Disable();
                    this["Expanded"].Enable();
                    ModeText.Text = "This mode is Clan Generator at it's most basic."
                    + "\nThe player will not be expected to manage the minutia of Clan life."
                    + "\nPerfect for a relaxing game session or for focusing on storytelling."
                    + "\nWith this mode you are the eye in the sky, watching the Clan as their story unfolds.";
                }
            );

            this["Expanded"] = new Element(
                94, 320, 162, 34,
                Context.Images["Buttons\\expanded_mode.png"],
                Context.Images["Buttons\\expanded_mode_hover.png"],
                Context.Images["Buttons\\expanded_mode_unavailable.png"],
                (s, e) =>
                {
                    Context.Clan.Gamemode = GamemodeType.Expanded;
                    this["Classic"].Enable();
                    this["Expanded"].Disable();
                    ModeText.Text = "A more hands-on experience. "
                    + "\nThis mode has everything in Classic Mode as well as more management-focused features.<br><br>"
                    + "\nNew features include:<br>"
                    + "\n- Illnesses, Injuries, and Permanent Conditions<br>"
                    + "\n- Herb gathering and treatment<br>"
                    + "\n- Ability to choose patrol type<br><br>"
                    + "\nWith this mode you'll be making the important Clan-life decisions.";
                }
            );

            this["Cruel"] = new Element(
                100, 400, 150, 30,
                Context.Images["Buttons\\cruel_season.png"],
                Context.Images["Buttons\\cruel_season_hover.png"]
            );

            this["MainMenu"] = new Element(
                25, 50, 153, 30,
                Context.Images["Buttons\\main_menu.png"],
                Context.Images["Buttons\\main_menu_hover.png"],
                Context.Images["Buttons\\main_menu_unavailable.png"],
                (s, e) => Context.Screens.SetScreen("Menu")
             );

            this["PreviousStep"] = new Element(
                253, 620, 147, 30,
                Context.Images["Buttons\\last_step.png"],
                Context.Images["Buttons\\last_step_hover.png"],
                Context.Images["Buttons\\last_step_unavailable.png"]
            );

            this["NextStep"] = new Element(
                400, 620, 147, 30,
                Context.Images["Buttons\\next_step.png"],
                Context.Images["Buttons\\next_step_hover.png"],
                Context.Images["Buttons\\next_step_unavailable.png"]
            );

            this["Classic"].ClickEvent();
        }

        public void NextStep()
        {
            
        }
    }


    public class Settings : Screen
    {
        public override void OnOpen()
        {
            this["ButtonSettings"] = new Element(
                100, 100, 150, 30,
                Context.Images["Buttons\\general_settings.png"],
                Context.Images["Buttons\\general_settings_hover.png"],
                Context.Images["Buttons\\general_settings_unavailable.png"]
            );
            this["ButtonSettingsSave"] = new Element(
                327, 550, 146, 30,
                Context.Images["Buttons\\save_settings.png"],
                Context.Images["Buttons\\save_settings_hover.png"],
                Context.Images["Buttons\\save_settings_unavailable.png"]
            );
            this["ButtonSettingsRelation"] = new Element(
                250, 100, 150, 30,
                Context.Images["Buttons\\relation_settings.png"],
                Context.Images["Buttons\\relation_settings_hover.png"],
                Context.Images["Buttons\\relation_settings_unavailable.png"]
            );
            this["ButtonLanguage"] = new Element(
                550, 100, 150, 30,
                Context.Images["Buttons\\language.png"],
                Context.Images["Buttons\\language_hover.png"],
                Context.Images["Buttons\\language_unavailable.png"]
            );
            this["ButtonInfo"] = new Element(
                400, 100, 150, 30,
                Context.Images["Buttons\\info.png"],
                Context.Images["Buttons\\info_hover.png"],
                Context.Images["Buttons\\info_unavailable.png"]
            );
            this["ButtonMainMenu"] = new Element(
                25, 25, 150, 30,
                Context.Images["Buttons\\main_menu.png"],
                Context.Images["Buttons\\main_menu_hover.png"],
                Context.Images["Buttons\\main_menu_unavailable.png"],
                (e, s) => Context.Screens.SetScreen("Menu")
            );
        }
    }
}
