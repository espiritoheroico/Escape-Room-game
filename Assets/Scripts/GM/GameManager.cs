using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    sealed class StateController
    {
        private static StateController gm = null;

        public static StateController GM
        {
            get
            {
                if (gm == null) gm = new StateController(); return gm;
            }
        }
        public enum gameStates { playGame, pauseGame, UI, directorsCut,inventory,puzzle}
        private static gameStates st = new gameStates();
        public static gameStates State
        {
            get { return st;}
            set { st = value; } 
        }

        
    }
    sealed class UniversalGameTimer
    {
        private static UniversalGameTimer ugt = null;
        public static UniversalGameTimer UGT
        {
            get
            {
                if (ugt == null) ugt = new UniversalGameTimer(); return ugt;
            }
        }
        public enum universalTimeStates {run,stop}
        private static universalTimeStates ust = new universalTimeStates();
        public static universalTimeStates UST
        {
            get { return ust; }
            set { ust = value; }
        }
    }
    public class CountTimer : MonoBehaviour
    {
        bool isCounting = false;
        public bool inverseCounting = false;
        public float countTimer = 0;
        public enum ts { run, stop, reset }
        public ts timestate;
        void Update()
        {
            if (UniversalGameTimer.UST == UniversalGameTimer.universalTimeStates.run)
            {
                if (isCounting) countTimer = inverseCounting == true ? countTimer -= Time.deltaTime : countTimer += Time.deltaTime;
            }
            Count();
        }
        public void Count()
        {
            switch (timestate)
            {
                case ts.run:
                    isCounting = true;
                    break;
                case ts.stop:
                    isCounting = false;
                    break;
                case ts.reset:
                    countTimer = 0;
                    break;
                default:
                    break;
            }
        }
    }

    public class GameManager : MonoBehaviour
    {
        //objs that we will access
        #region accessables
        [SerializeField] GameObject scene_Camera;
        [SerializeField] GameObject player_Camera;
        //canvases
        [SerializeField] GameObject canvas_UI;
        [SerializeField] GameObject canvas_HUD;
        [SerializeField] GameObject canvas_INVENTARY;
        [SerializeField] GameObject canvas_PUZZLE;
        #endregion

        #region playtime
        float gamePlayedTime = 0;
        CountTimer ct;
        #endregion

        //control timeline
        #region director
        [SerializeField]GameObject director;
        #endregion

        void Start()
        {
            ct = this.gameObject.AddComponent<CountTimer>();
            ct.timestate = CountTimer.ts.run;
            gamePlayedTime = ct.countTimer;
            StateController.State = StateController.gameStates.UI;
        }

        void Update()
        {
            CareAboutStates();
        }

        //this just care about states out of UPDATE.
        void CareAboutStates()
        {
            switch (StateController.State)
            {
                case StateController.gameStates.playGame:
                    canvas_HUD.SetActive(true);
                    canvas_UI.SetActive(false);
                    canvas_INVENTARY.SetActive(false);
                    canvas_PUZZLE.SetActive(false);
                    UniversalGameTimer.UST = UniversalGameTimer.universalTimeStates.run;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case StateController.gameStates.pauseGame:
                    canvas_HUD.SetActive(true);
                    canvas_UI.SetActive(false);
                    canvas_INVENTARY.SetActive(false);
                    canvas_PUZZLE.SetActive(false);
                    UniversalGameTimer.UST = UniversalGameTimer.universalTimeStates.stop;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case StateController.gameStates.UI:
                    canvas_HUD.SetActive(false);
                    canvas_UI.SetActive(true);
                    canvas_INVENTARY.SetActive(false);
                    canvas_PUZZLE.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case StateController.gameStates.directorsCut:
                    canvas_HUD.SetActive(false);
                    canvas_UI.SetActive(false);
                    canvas_INVENTARY.SetActive(false);
                    canvas_PUZZLE.SetActive(false);
                    UniversalGameTimer.UST = UniversalGameTimer.universalTimeStates.stop;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case StateController.gameStates.inventory:
                    canvas_HUD.SetActive(false);
                    canvas_UI.SetActive(false);
                    canvas_INVENTARY.SetActive(true);
                    canvas_PUZZLE.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case StateController.gameStates.puzzle:
                    canvas_HUD.SetActive(false);
                    canvas_UI.SetActive(false);
                    canvas_INVENTARY.SetActive(false);
                    canvas_PUZZLE.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    break;
            }
        }
    }
}