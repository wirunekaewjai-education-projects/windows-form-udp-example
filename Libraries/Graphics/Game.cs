using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libraries
{
    public class Game
    {
        public static int WINDOW_WIDTH = 640;
        public static int WINDOW_HEIGHT = 480;

        private Scene currentScene;
        private Form currentForm;

        private Font font;
        
        private Game()
        {
            font = new Font("Tahoma", 8);
        }


        public static Font GetFont()
        {
            return Instance.font;
        }

        public static void SetForm(Form form)
        {
            Game game = Instance;
            game.currentForm = form;
        }

        public static void SetScene(Scene scene)
        {
            Game game = Instance;
            Form form = game.currentForm;

            if (null != game.currentScene)
            {
                Action<Control> actionRemove = (c) =>
                {
                    form.Controls.Remove(c);
                    game.currentScene.OnDestroy();
                };

                form.Invoke(actionRemove, game.currentScene);
            }

            //
            game.currentScene = scene;

            Action<Control> actionAdd = (c) =>
            {
                form.Controls.Add(c);
            };

            form.Invoke(actionAdd, game.currentScene);
        }

        public static void Destroy()
        {
            if (null != instance)
            {
                Game game = instance;
                
                if(game.currentScene != null)
                    game.currentScene.OnDestroy();

                game.currentScene = null;
                game.currentForm = null;
                game.font = null;

                instance = null;
            }
        }

        private static Game instance = null;
        private static Game Instance
        {
            get
            {
                if (instance == null)
                    instance = new Game();

                return instance;
            }
        }
    }
}
