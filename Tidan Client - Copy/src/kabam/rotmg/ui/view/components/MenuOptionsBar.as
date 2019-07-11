package kabam.rotmg.ui.view.components {
import com.company.assembleegameclient.screens.TitleMenuOption;
import com.company.rotmg.graphics.ScreenGraphic;

import flash.display.Sprite;
import flash.geom.Rectangle;

public class MenuOptionsBar extends Sprite {

    private static const Y_POSITION:Number = 550;
    private static const SPACING:int = 20;
    public static const CENTER:String = "CENTER";
    public static const RIGHT:String = "RIGHT";
    public static const LEFT:String = "LEFT";
    public static const PLAY:String = "PLAY";
    public static const SERVER:String = "SERVER";
    public static const ACCOUNT:String = "ACCOUNT";
    public static const SUPPORT:String = "SUPPORT";
    public static const LEGENDS:String = "LEGENDS";
   // public static const EDITOR:String = "EDITOR";
    private const leftObjects:Array = [];
    private const rightObjects:Array = [];

    private var screenGraphic:ScreenGraphic;

    public function MenuOptionsBar() {
        this.makeScreenGraphic();
    }

    private function makeScreenGraphic():void {
        this.screenGraphic = new ScreenGraphic();
        addChild(this.screenGraphic);
    }

    public function addButton(_arg_1:TitleMenuOption, _arg_2:String):void {
        this.screenGraphic.addChild(_arg_1);
        switch (_arg_2) {
            case PLAY:
                this.leftObjects[0] = (this.rightObjects[0] = _arg_1);
                _arg_1.x = 405;
                _arg_1.y = 380;
                return;
            case SERVER:
                this.leftObjects[0] = (this.rightObjects[0] = _arg_1);
                _arg_1.x = 440;
                _arg_1.y = 420;
                return;
            case ACCOUNT:
                this.leftObjects[0] = (this.rightObjects[0] = _arg_1);
                _arg_1.x = 360;
                _arg_1.y = 440;
                return;
            case LEGENDS:
                this.leftObjects[0] = (this.rightObjects[0] = _arg_1);
                _arg_1.x = 360;
                _arg_1.y = 460;
                return;
            case SUPPORT:
                this.leftObjects[0] = (this.rightObjects[0] = _arg_1);
                _arg_1.x = 445;
                _arg_1.y = 480;
                return;

      //      case EDITOR:
       //         this.leftObjects[0] = (this.rightObjects[0] = _arg_1);
       //         _arg_1.x = 50;
        //        _arg_1.y = 520;
        //        return;
            case CENTER:
                this.leftObjects[0] = (this.rightObjects[0] = _arg_1);
                _arg_1.x = (this.screenGraphic.width / 2);
                _arg_1.y = Y_POSITION;
                return;
            case LEFT:
                this.layoutToLeftOf(this.leftObjects[(this.leftObjects.length - 1)], _arg_1);
                this.leftObjects.push(_arg_1);
                _arg_1.changed.add(this.layoutLeftButtons);
                return;
            case RIGHT:
                this.layoutToRightOf(this.rightObjects[(this.rightObjects.length - 1)], _arg_1);
                this.rightObjects.push(_arg_1);
                _arg_1.changed.add(this.layoutRightButtons);
                return;
        }
    }

    private function layoutLeftButtons():void {
        var _local_1:int = 1;
        while (_local_1 < this.leftObjects.length) {
            this.layoutToLeftOf(this.leftObjects[(_local_1 - 1)], this.leftObjects[_local_1]);
            _local_1++;
        }
    }

    private function layoutToLeftOf(_arg_1:TitleMenuOption, _arg_2:TitleMenuOption):void {
        var _local_3:Rectangle = _arg_1.getBounds(_arg_1);
        var _local_4:Rectangle = _arg_2.getBounds(_arg_2);
        _arg_2.x = (((_arg_1.x + _local_3.left) - _local_4.right) - SPACING);
        _arg_2.y = Y_POSITION;
    }

    private function layoutRightButtons():void {
        var _local_1:int = 1;
        while (_local_1 < this.rightObjects.length) {
            this.layoutToRightOf(this.rightObjects[(_local_1 - 1)], this.rightObjects[_local_1]);
            _local_1++;
        }
    }

    private function layoutToRightOf(_arg_1:TitleMenuOption, _arg_2:TitleMenuOption):void {
        var _local_3:Rectangle = _arg_1.getBounds(_arg_1);
        var _local_4:Rectangle = _arg_2.getBounds(_arg_2);
        _arg_2.x = (((_arg_1.x + _local_3.right) - _local_4.left) + SPACING);
        _arg_2.y = Y_POSITION;
    }


}
}//package kabam.rotmg.ui.view.components
