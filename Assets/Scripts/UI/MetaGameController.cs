using Platformer.Mechanics;
using Platformer.Model;
using Platformer.Core;
using UnityEngine;
using System.Collections;

namespace Platformer.UI
{
    /// <summary>
    /// The MetaGameController is responsible for switching control between the high level
    /// contexts of the application, eg the Main Menu and Gameplay systems.
    /// </summary>
    public class MetaGameController : MonoBehaviour
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        /// <summary>
        /// The main UI object which used for the menu.
        /// </summary>
        public MainUIController mainMenu;
        /// <summary>
        /// Victory UI
        /// </summary>
        public VictoryUIController victoryUIController;
        /// <summary>
        /// Deskripsi UI
        /// </summary>
        public ChestInstance deskripsiUIControl;
        /// <summary>
        /// A list of canvas objects which are used during gameplay (when the main ui is turned off)
        /// </summary>
        public Canvas[] gamePlayCanvasii;
        /// <summary>
        /// The game controller.
        /// </summary>
        public GameController gameController;
        /// <summary>
        /// Boolean
        /// </summary>
        bool showMainCanvas = false;
        bool showVictoryCanvas = false;
        bool showOffDeskripsiCanvas = false;
        bool showOnDeskripsiCanvas = false;
        /// <summary>
        /// OnEnable Function
        /// </summary>
        void OnEnable()
        {
            _ToggleMainMenu(showMainCanvas);
            _ToggleVictoryCanvas(showVictoryCanvas);
            _OffToggleDeskripsiCanvas(showOffDeskripsiCanvas);
            _OnToggleDeskripsiCanvas(showOnDeskripsiCanvas);
        }

        /// <summary>
        /// Turn the main menu on or off.
        /// </summary>
        /// <param name="show"></param>
        /// 
        public void isToggleMainMenu()
        {
            ToggleMainMenu(show: !showMainCanvas);
        }
        public void ToggleMainMenu(bool show)
        {
            if (this.showMainCanvas != show)
            {
                _ToggleMainMenu(show);
            }
        }

        void _ToggleMainMenu(bool show)
        {
            if (show)
            {
                Time.timeScale = 0;
                model.player.controlEnabled = false;
                mainMenu.gameObject.SetActive(true);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                model.player.controlEnabled = true;
                mainMenu.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
            }
            this.showMainCanvas = show;
        }

        /// <summary>
        /// show Victory Canvas
        /// </summary>
        /// <param name="show"></param>

        public void ToggleVictoryCanvas(bool show)
        {
            if (this.showVictoryCanvas != show)
            {
                _ToggleVictoryCanvas(show);
            }
        }

        void _ToggleVictoryCanvas(bool show)
        {
            if (show)
            {
                StartCoroutine(StartUIVictory());
            }
            else
            {
                victoryUIController.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
            }
            this.showVictoryCanvas = show;
        }
        /// <summary>
        /// Show Off Deskripsi UI
        /// </summary>
        public void OffToggleDeskripsiCanvas()
        {
            OffToggleDeskripsiCanvas(show: !showOffDeskripsiCanvas);
        }
        void OffToggleDeskripsiCanvas(bool show)
        {
            if (this.showOffDeskripsiCanvas != show)
            {
                _OffToggleDeskripsiCanvas(show);
            }
        }
        void _OffToggleDeskripsiCanvas(bool show)
        {
            if (show)
            {
                showOnDeskripsiCanvas = false;
                deskripsiUIControl.UIDeskripsi.SetActive(false);
                deskripsiUIControl.animator.SetTrigger("close");
                model.player.controlEnabled = true;
            }
            //else
            //{
            //    this.showOnDeskripsiCanvas = true;
            //}
            this.showOffDeskripsiCanvas = show;
        }
        /// <summary>
        /// Show On Deskripsi UI
        /// </summary>
        public void ToggleDeskripsiCanvas()
        {
            OnToggleDeskripsiCanvas(show: !showOnDeskripsiCanvas);
        }
        void OnToggleDeskripsiCanvas(bool show)
        {
            if (this.showOnDeskripsiCanvas != show)
            {
                _OnToggleDeskripsiCanvas(show);
            }
        }
        void _OnToggleDeskripsiCanvas(bool show)
        {
            if (show)
            {
                showOffDeskripsiCanvas = false;
                StartCoroutine(OpenUIDeskripsi());
            }
            else
            {
                
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
            }
            this.showOnDeskripsiCanvas = show;
        }

        void Update()
        {
            if (Input.GetButtonDown("Menu"))
            {
                isToggleMainMenu();
            }
        }
        /// <summary>
        /// Start UI Victory
        /// </summary>
        /// <returns></returns>
        IEnumerator StartUIVictory()
        {
            yield return new WaitForSeconds(2f);
            victoryUIController.gameObject.SetActive(true);
            foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
        }
        /// <summary>
        /// Open UIDeskripsi
        /// </summary>
        /// <returns></returns>
        IEnumerator OpenUIDeskripsi()
        {
            deskripsiUIControl.animator.SetTrigger("open");

            yield return new WaitForSeconds(1f);
            deskripsiUIControl.UIDeskripsi.SetActive(true);
            foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
        }

    }
}
