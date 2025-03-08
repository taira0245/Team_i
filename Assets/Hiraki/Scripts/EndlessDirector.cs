using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessDirector : StageDirector
{
    protected override bool GameStageExe()
    {
        if (plDirector_.CheckChangeCount()) { killCounter_.KillCountPlus(); }
        if (plDirector_.CheckChangeHP()) {
            hitPoint_.Damage();
            if (plDirector_.CurrentHP <= 0) {
                gameover_Flag = true;
                GameTerminate();
                return false;
            }
        }

        return true;

    }
}
