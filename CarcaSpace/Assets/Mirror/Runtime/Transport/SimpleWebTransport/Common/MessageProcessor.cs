<<<<<<< HEAD
using System;
=======
>>>>>>> origin/alpha_merge
using System.IO;
using System.Runtime.CompilerServices;

namespace Mirror.SimpleWeb
{
    public static class MessageProcessor
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static byte FirstLengthByte(byte[] buffer) => (byte)(buffer[1] & 0b0111_1111);

<<<<<<< HEAD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
=======
>>>>>>> origin/alpha_merge
        public static bool NeedToReadShortLength(byte[] buffer)
        {
            byte lenByte = FirstLengthByte(buffer);

<<<<<<< HEAD
            return lenByte == Constants.UshortPayloadLength;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NeedToReadLongLength(byte[] buffer)
        {
            byte lenByte = FirstLengthByte(buffer);

            return lenByte == Constants.UlongPayloadLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
=======
            return lenByte >= Constants.UshortPayloadLength;
        }

>>>>>>> origin/alpha_merge
        public static int GetOpcode(byte[] buffer)
        {
            return buffer[0] & 0b0000_1111;
        }

<<<<<<< HEAD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
=======
>>>>>>> origin/alpha_merge
        public static int GetPayloadLength(byte[] buffer)
        {
            byte lenByte = FirstLengthByte(buffer);
            return GetMessageLength(buffer, 0, lenByte);
        }

<<<<<<< HEAD
        /// <summary>
        /// Has full message been sent
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Finished(byte[] buffer)
        {
            return (buffer[0] & 0b1000_0000) != 0;
        }

        public static void ValidateHeader(byte[] buffer, int maxLength, bool expectMask, bool opCodeContinuation = false)
        {
            bool finished = Finished(buffer);
=======
        public static void ValidateHeader(byte[] buffer, int maxLength, bool expectMask)
        {
            bool finished = (buffer[0] & 0b1000_0000) != 0; // has full message been sent
>>>>>>> origin/alpha_merge
            bool hasMask = (buffer[1] & 0b1000_0000) != 0; // true from clients, false from server, "All messages from the client to the server have this bit set"

            int opcode = buffer[0] & 0b0000_1111; // expecting 1 - text message
            byte lenByte = FirstLengthByte(buffer);

<<<<<<< HEAD
            ThrowIfMaskNotExpected(hasMask, expectMask);
            ThrowIfBadOpCode(opcode, finished, opCodeContinuation);
=======
            ThrowIfNotFinished(finished);
            ThrowIfMaskNotExpected(hasMask, expectMask);
            ThrowIfBadOpCode(opcode);
>>>>>>> origin/alpha_merge

            int msglen = GetMessageLength(buffer, 0, lenByte);

            ThrowIfLengthZero(msglen);
            ThrowIfMsgLengthTooLong(msglen, maxLength);
        }

<<<<<<< HEAD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
=======
>>>>>>> origin/alpha_merge
        public static void ToggleMask(byte[] src, int sourceOffset, int messageLength, byte[] maskBuffer, int maskOffset)
        {
            ToggleMask(src, sourceOffset, src, sourceOffset, messageLength, maskBuffer, maskOffset);
        }

<<<<<<< HEAD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
=======
>>>>>>> origin/alpha_merge
        public static void ToggleMask(byte[] src, int sourceOffset, ArrayBuffer dst, int messageLength, byte[] maskBuffer, int maskOffset)
        {
            ToggleMask(src, sourceOffset, dst.array, 0, messageLength, maskBuffer, maskOffset);
            dst.count = messageLength;
        }

<<<<<<< HEAD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
=======
>>>>>>> origin/alpha_merge
        public static void ToggleMask(byte[] src, int srcOffset, byte[] dst, int dstOffset, int messageLength, byte[] maskBuffer, int maskOffset)
        {
            for (int i = 0; i < messageLength; i++)
            {
                byte maskByte = maskBuffer[maskOffset + i % Constants.MaskSize];
                dst[dstOffset + i] = (byte)(src[srcOffset + i] ^ maskByte);
            }
        }

        /// <exception cref="InvalidDataException"></exception>
        static int GetMessageLength(byte[] buffer, int offset, byte lenByte)
        {
            if (lenByte == Constants.UshortPayloadLength)
            {
<<<<<<< HEAD
                // header is 2 bytes
=======
                // header is 4 bytes long
>>>>>>> origin/alpha_merge
                ushort value = 0;
                value |= (ushort)(buffer[offset + 2] << 8);
                value |= buffer[offset + 3];

                return value;
            }
            else if (lenByte == Constants.UlongPayloadLength)
            {
<<<<<<< HEAD
                // header is 8 bytes 
                ulong value = 0;
                value |= ((ulong)buffer[offset + 2] << 56);
                value |= ((ulong)buffer[offset + 3] << 48);
                value |= ((ulong)buffer[offset + 4] << 40);
                value |= ((ulong)buffer[offset + 5] << 32);
                value |= ((ulong)buffer[offset + 6] << 24);
                value |= ((ulong)buffer[offset + 7] << 16);
                value |= ((ulong)buffer[offset + 8] << 8);
                value |= ((ulong)buffer[offset + 9] << 0);

                if (value > int.MaxValue)
                {
                    throw new NotSupportedException($"Can't receive payloads larger that int.max: {int.MaxValue}");
                }
                return (int)value;
=======
                throw new InvalidDataException("Max length is longer than allowed in a single message");
>>>>>>> origin/alpha_merge
            }
            else // is less than 126
            {
                // header is 2 bytes long
                return lenByte;
            }
        }

        /// <exception cref="InvalidDataException"></exception>
<<<<<<< HEAD
=======
        static void ThrowIfNotFinished(bool finished)
        {
            if (!finished)
            {
                throw new InvalidDataException("Full message should have been sent, if the full message wasn't sent it wasn't sent from this trasnport");
            }
        }

        /// <exception cref="InvalidDataException"></exception>
>>>>>>> origin/alpha_merge
        static void ThrowIfMaskNotExpected(bool hasMask, bool expectMask)
        {
            if (hasMask != expectMask)
            {
                throw new InvalidDataException($"Message expected mask to be {expectMask} but was {hasMask}");
            }
        }

        /// <exception cref="InvalidDataException"></exception>
<<<<<<< HEAD
        static void ThrowIfBadOpCode(int opcode, bool finished, bool opCodeContinuation)
        {
            // 0 = continuation
            // 2 = binary
            // 8 = close

            // do we expect Continuation?
            if (opCodeContinuation)
            {
                // good it was Continuation
                if (opcode == 0)
                    return;

                // bad, wasn't Continuation
                throw new InvalidDataException("Expected opcode to be Continuation");
            }
            else if (!finished)
            {
                // fragmented message, should be binary
                if (opcode == 2)
                    return;

                throw new InvalidDataException("Expected opcode to be binary");
            }
            else
            {
                // normal message, should be binary or close
                if (opcode == 2 || opcode == 8)
                    return;

=======
        static void ThrowIfBadOpCode(int opcode)
        {
            // 2 = binary
            // 8 = close
            if (opcode != 2 && opcode != 8)
            {
>>>>>>> origin/alpha_merge
                throw new InvalidDataException("Expected opcode to be binary or close");
            }
        }

        /// <exception cref="InvalidDataException"></exception>
        static void ThrowIfLengthZero(int msglen)
        {
            if (msglen == 0)
            {
                throw new InvalidDataException("Message length was zero");
            }
        }

        /// <summary>
        /// need to check this so that data from previous buffer isn't used
        /// </summary>
<<<<<<< HEAD
        public static void ThrowIfMsgLengthTooLong(int msglen, int maxLength)
=======
        /// <exception cref="InvalidDataException"></exception>
        static void ThrowIfMsgLengthTooLong(int msglen, int maxLength)
>>>>>>> origin/alpha_merge
        {
            if (msglen > maxLength)
            {
                throw new InvalidDataException("Message length is greater than max length");
            }
        }
    }
}
