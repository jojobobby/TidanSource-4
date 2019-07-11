package kabam.rotmg.messaging.impl.incoming {

import flash.utils.IDataInput;

public class LootOpen extends IncomingMessage{

    public var itemId_:uint;

    public function LootOpen(_arg1:uint, _arg2:Function) {
        super(_arg1, _arg2);
    }

    override public function parseFromInput(_arg1:IDataInput):void {
        this.itemId_ = _arg1.readUnsignedInt();
    }

    override public function toString():String {
        return (formatToString("REWARD", "ItemId_"));
    }
}
}
