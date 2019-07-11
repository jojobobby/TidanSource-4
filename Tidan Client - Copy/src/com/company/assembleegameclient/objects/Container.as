package com.company.assembleegameclient.objects {
import com.company.assembleegameclient.game.GameSprite;
import com.company.assembleegameclient.map.Camera;
import com.company.assembleegameclient.map.Map;
import com.company.assembleegameclient.parameters.Parameters;
import com.company.assembleegameclient.sound.SoundEffectLibrary;
import com.company.assembleegameclient.ui.options.Options;
import com.company.assembleegameclient.ui.panels.Panel;
import com.company.assembleegameclient.ui.panels.itemgrids.ContainerGrid;
import com.company.util.PointUtil;
import com.company.util.GraphicsUtil;

import flash.display.BitmapData;
import flash.display.GraphicsBitmapFill;
import flash.display.GraphicsPath;
import flash.display.IGraphicsData;
import flash.geom.Matrix;

public class Container extends GameObject implements IInteractiveObject {

    public var isLoot_:Boolean;
    public var ownerId_:String;
    public var drawMeBig_:Boolean;
    private var lastEquips:String = "rebuild";

    public function Container(_arg1:XML) {
        super(_arg1);
        isInteractive_ = true;
        this.isLoot_ = _arg1.hasOwnProperty("Loot");
        this.ownerId_ = "";
    }

    public function setOwnerId(_arg1:String):void {
        this.ownerId_ = _arg1;
        isInteractive_ = (((this.ownerId_ == "")) || (this.isBoundToCurrentAccount()));
    }

    public function isBoundToCurrentAccount():Boolean {
        return ((map_.player_.accountId_ == this.ownerId_));
    }

    override public function addTo(_arg1:Map, _arg2:Number, _arg3:Number):Boolean {
        if (!super.addTo(_arg1, _arg2, _arg3)) {
            return (false);
        }
        if (map_.player_ == null) {
            return (true);
        }
        var _local4:Number = PointUtil.distanceXY(map_.player_.x_, map_.player_.y_, _arg2, _arg3);
        if (((this.isLoot_) && ((_local4 < 12)))) {
            SoundEffectLibrary.play("loot_appears");
        }
        if(this.shouldSendBag(this.objectType_))
        {
            this.drawMeBig_ = true;
        }
        return (true);
    }

    public function getPanel(_arg1:GameSprite):Panel {
        var _local2:Player = ((((_arg1) && (_arg1.map))) ? _arg1.map.player_ : null);
        return (new ContainerGrid(this, _local2));
    }

    override public function draw(param1:Vector.<IGraphicsData>, param2:Camera, param3:int) : void
    {
        super.draw(param1,param2,param3);
        if(Parameters.data_.lootPreview)
        {
            this.drawItems(param1,param2,param3);
        }
    }

    public function updateItemSprites(param1:Vector.<BitmapData>) : void
    {
        var _loc2_:uint = 0;
        var _loc3_:* = null;
        var _loc4_:int = -1;
        var _loc5_:uint = this.equipment_.length;
        _loc2_ = 0;
        while(_loc2_ < _loc5_)
        {
            _loc4_ = this.equipment_[_loc2_];
            _loc3_ = ObjectLibrary.getItemIcon(_loc4_);
            param1.push(_loc3_);
            _loc2_++;
        }
    }
    private function shouldSendBag(param1:int) : Boolean
    {
        return param1 >= 1287 && param1 <= 1289 || param1 == 1291 || param1 == 1292 || param1 >= 1294 && param1 <= 1296 || param1 == 1708 || param1 >= 1722 && param1 <= 1728;
    }
    public function drawItems(param1:Vector.<IGraphicsData>, param2:Camera, param3:int) : void
    {
        var _loc7_:Number = NaN;
        var _loc8_:Number = NaN;
        var _loc11_:int = 0;
        var _loc12_:int = 0;
        var _loc4_:* = null;
        var _loc5_:* = null;
        var _loc6_:* = null;
        var _loc9_:* = null;
        var _loc10_:* = null;
        //   if(Options)
        //  {
        //      return;
        //   }
        if(this.icons_ == null)
        {
            this.icons_ = new Vector.<BitmapData>();
            this.iconFills_ = new Vector.<GraphicsBitmapFill>();
            this.iconPaths_ = new Vector.<GraphicsPath>();
            this.icons_.length = 0;
            this.updateItemSprites(this.icons_);
        }
        else
        {
            _loc10_ = String(this.equipment_);
            if(_loc10_ != this.lastEquips)
            {
                this.icons_.length = 0;
                this.lastEquips = _loc10_;
                this.updateItemSprites(this.icons_);
            }
        }
        var _loc13_:Number = posS_[3];
        var _loc14_:Number = this.vS_[1];
        _loc11_ = 0;
        while(_loc11_ < this.icons_.length)
        {
            _loc4_ = this.icons_[_loc11_];
            if(_loc11_ >= this.iconFills_.length)
            {
                this.iconFills_.push(new GraphicsBitmapFill(null,new Matrix(),false,false));
                this.iconPaths_.push(new GraphicsPath(GraphicsUtil.QUAD_COMMANDS,new Vector.<Number>()));
            }
            _loc5_ = this.iconFills_[_loc11_];
            _loc6_ = this.iconPaths_[_loc11_];
            _loc5_.bitmapData = _loc4_;
            _loc12_ = _loc11_ * 0.25;
            _loc7_ = _loc13_ - _loc4_.width * 2 + _loc11_ % 4 * _loc4_.width;
            _loc8_ = _loc14_ - _loc4_.height * 0.5 + _loc12_ * (_loc4_.height + 5) - (_loc12_ * 5 + 20);
            _loc6_.data.length = 0;
            _loc6_.data.push(_loc7_,_loc8_,_loc7_ + _loc4_.width,_loc8_,_loc7_ + _loc4_.width,_loc8_ + _loc4_.height,_loc7_,_loc8_ + _loc4_.height);
            _loc9_ = _loc5_.matrix;
            _loc9_.identity();
            _loc9_.translate(_loc7_,_loc8_);
            param1.push(_loc5_);
            param1.push(_loc6_);
            param1.push(GraphicsUtil.END_FILL);
            _loc11_++;
        }
    }
}
}
