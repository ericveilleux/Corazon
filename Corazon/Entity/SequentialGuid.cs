﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Corazon.Common
{
    /// <summary>
    /// A sequential guid improves the performance as there is less entropy in the generated keys
    /// </summary>
    /// <see cref="http://www.siepman.nl/blog/post/2015/06/20/SequentialGuid-Comb-Sql-Server-With-Creation-Date-Time-.aspx"/>
    [Serializable]
    internal struct SequentialGuid : IComparable<SequentialGuid>, IComparable<Guid>, IComparable
    {
        private const int NumberOfSequenceBytes = 6;
        private const int PermutationsOfAByte = 256;
        private static readonly long MaximumPermutations =
                (long)Math.Pow(PermutationsOfAByte, NumberOfSequenceBytes);
        private static long lastSequence;


        private static readonly DateTime SequencePeriodStart =
            new DateTime(2011, 11, 15, 0, 0, 0, DateTimeKind.Utc); // Start = 000000

        private static readonly DateTime SequencePeriodeEnd =
            new DateTime(2100, 1, 1, 0, 0, 0, DateTimeKind.Utc);   // End   = FFFFFF

        private readonly Guid _guidValue;

        public SequentialGuid(Guid guidValue)
        {
            this._guidValue = guidValue;
        }

        public SequentialGuid(string guidValue)
            : this(new Guid(guidValue))
        {
        }

        [System.Security.SecuritySafeCritical]
        public static SequentialGuid NewSequentialGuid()
        {
            // You might want to inject DateTime.Now in production code
            return new SequentialGuid(GetGuidValue(DateTime.Now));
        }

        public static TimeSpan TimePerSequence
        {
            get
            {
                var ticksPerSequence = TotalPeriod.Ticks / MaximumPermutations;
                var result = new TimeSpan(ticksPerSequence);
                return result;
            }
        }

        public static TimeSpan TotalPeriod
        {
            get
            {
                var result = SequencePeriodeEnd - SequencePeriodStart;
                return result;
            }
        }
        
        // Internal for testing
        internal static Guid GetGuidValue(DateTime now)
        {
            if (now < SequencePeriodStart || now >= SequencePeriodeEnd)
            {
                return Guid.NewGuid(); // Outside the range, use regular Guid
            }

            var sequence = GetCurrentSequence(now);
            return GetGuid(sequence);
        }

        private static long GetCurrentSequence(DateTime now)
        {
            var ticksUntilNow = now.Ticks - SequencePeriodStart.Ticks;
            var factor = (decimal)ticksUntilNow / TotalPeriod.Ticks;
            var resultDecimal = factor * MaximumPermutations;
            var resultLong = (long)resultDecimal;
            return resultLong;
        }

        private static readonly object SynchronizationObject = new object();
        private static Guid GetGuid(long sequence)
        {
            lock (SynchronizationObject)
            {
                if (sequence <= lastSequence)
                {
                    // Prevent double sequence on same server
                    sequence = lastSequence + 1;
                }
                lastSequence = sequence;
            }

            var sequenceBytes = GetSequenceBytes(sequence);
            var guidBytes = GetGuidBytes();
            var totalBytes = guidBytes.Concat(sequenceBytes).ToArray();
            var result = new Guid(totalBytes);
            return result;
        }

        private static IEnumerable<byte> GetSequenceBytes(long sequence)
        {
            var sequenceBytes = BitConverter.GetBytes(sequence);
            var sequenceBytesLongEnough = sequenceBytes.Concat(new byte[NumberOfSequenceBytes]);
            var result = sequenceBytesLongEnough.Take(NumberOfSequenceBytes).Reverse();
            return result;
        }

        private static IEnumerable<byte> GetGuidBytes()
        {
            return Guid.NewGuid().ToByteArray().Take(10);
        }
        
        public DateTime CreatedDateTime
        {
            get
            {
                return GetCreatedDateTime(this._guidValue);
            }
        }

        internal static DateTime GetCreatedDateTime(Guid value)
        {
            var sequenceBytes = GetSequenceLongBytes(value).ToArray();
            var sequenceLong = BitConverter.ToInt64(sequenceBytes, 0);
            var sequenceDecimal = (decimal)sequenceLong;
            var factor = sequenceDecimal / MaximumPermutations;
            var ticksUntilNow = factor * TotalPeriod.Ticks;
            var nowTicksDecimal = ticksUntilNow + SequencePeriodStart.Ticks;
            var nowTicks = (long)nowTicksDecimal;
            var result = new DateTime(nowTicks);
            return result;
        }

        private static IEnumerable<byte> GetSequenceLongBytes(Guid value)
        {
            const int NumberOfBytesOfLong = 8;
            var sequenceBytes = value.ToByteArray().Skip(10).Reverse().ToArray();
            var additionalBytesCount = NumberOfBytesOfLong - sequenceBytes.Length;
            return sequenceBytes.Concat(new byte[additionalBytesCount]);
        }
        
        public static bool operator <(SequentialGuid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) < 0;
        }

        public static bool operator >(SequentialGuid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) > 0;
        }

        public static bool operator <(Guid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) < 0;
        }

        public static bool operator >(Guid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) > 0;
        }

        public static bool operator <(SequentialGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) < 0;
        }

        public static bool operator >(SequentialGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) > 0;
        }

        public static bool operator <=(SequentialGuid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) <= 0;
        }

        public static bool operator >=(SequentialGuid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) >= 0;
        }

        public static bool operator <=(Guid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) <= 0;
        }

        public static bool operator >=(Guid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) >= 0;
        }

        public static bool operator <=(SequentialGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) <= 0;
        }

        public static bool operator >=(SequentialGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) >= 0;
        }
        
        public static bool operator ==(SequentialGuid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) == 0;
        }

        public static bool operator !=(SequentialGuid value1, SequentialGuid value2)
        {
            return !(value1 == value2);
        }

        public static bool operator ==(Guid value1, SequentialGuid value2)
        {
            return value1.CompareTo(value2) == 0;
        }

        public static bool operator !=(Guid value1, SequentialGuid value2)
        {
            return !(value1 == value2);
        }

        public static bool operator ==(SequentialGuid value1, Guid value2)
        {
            return value1.CompareTo(value2) == 0;
        }

        public static bool operator !=(SequentialGuid value1, Guid value2)
        {
            return !(value1 == value2);
        }
        
        public int CompareTo(object obj)
        {
            if (obj is SequentialGuid)
            {
                return this.CompareTo((SequentialGuid)obj);
            }
            if (obj is Guid)
            {
                return this.CompareTo((Guid)obj);
            }
            throw new ArgumentException("Parameter is not of the rigt type");
        }

        public int CompareTo(SequentialGuid other)
        {
            return this.CompareTo(other._guidValue);
        }

        public int CompareTo(Guid other)
        {
            return CompareImplementation(this._guidValue, other);
        }

        private static readonly int[] IndexOrderingHighLow =
                                      { 10, 11, 12, 13, 14, 15, 8, 9, 7, 6, 5, 4, 3, 2, 1, 0 };

        private static int CompareImplementation(Guid left, Guid right)
        {
            var leftBytes = left.ToByteArray();
            var rightBytes = right.ToByteArray();

            return IndexOrderingHighLow.Select(i => leftBytes[i].CompareTo(rightBytes[i]))
                                       .FirstOrDefault(r => r != 0);
        }
        
        public override bool Equals(Object obj)
        {
            if (obj is SequentialGuid || obj is Guid)
            {
                return this.CompareTo(obj) == 0;
            }

            return false;
        }

        public bool Equals(SequentialGuid other)
        {
            return this.CompareTo(other) == 0;
        }

        public bool Equals(Guid other)
        {
            return this.CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return this._guidValue.GetHashCode();
        }
        
        public static implicit operator Guid(SequentialGuid value)
        {
            return value._guidValue;
        }

        public static explicit operator SequentialGuid(Guid value)
        {
            return new SequentialGuid(value);
        }

        public override string ToString()
        {
            var roundedCreatedDateTime = Round(this.CreatedDateTime, TimeSpan.FromMilliseconds(1));
            return string.Format("{0} ({1:yyyy-MM-dd HH:mm:ss.fff})", this._guidValue, roundedCreatedDateTime);
        }

        private static DateTime Round(DateTime dateTime, TimeSpan interval)
        {
            var halfIntervalTicks = (interval.Ticks + 1) >> 1;

            return dateTime.AddTicks(halfIntervalTicks -
                   ((dateTime.Ticks + halfIntervalTicks) % interval.Ticks));
        }
    }
}