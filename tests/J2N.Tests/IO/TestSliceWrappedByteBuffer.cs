﻿namespace J2N.IO
{
    public class TestSliceWrappedByteBuffer : TestWrappedByteBuffer
    {
        public override void SetUp()
        {
            base.SetUp();
            buf.SetPosition(1).SetLimit(BUFFER_LENGTH - 1);
            buf = buf.Slice();
            baseBuf = buf;
        }

        public override void TearDown()
        {
            base.TearDown();
        }
    }
}
