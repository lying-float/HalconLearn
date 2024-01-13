using System.Collections.Generic;

namespace CENJE_Vison
{

	public class PoNegativeResult
	{
		public Queue<bool> negativeResult;

		public Queue<bool> positiveResult;

		private object lock_negative = new object();

		private object lock_positive = new object();

		public int negativeInterval;

		public int positiveInterval;

		public void GetNPInterval(int negativeInterval, int positiveInterval)
		{
			if (negativeInterval == 0)
			{
				negativeInterval = 1;
			}
			else
			{
				this.negativeInterval = negativeInterval;
			}
			if (positiveInterval == 0)
			{
				positiveInterval = 1;
			}
			else
			{
				this.positiveInterval = positiveInterval;
			}
		}

		public void InitialNegativeData()
		{
			negativeResult = new Queue<bool>(negativeInterval);
			for (int i = 0; i < negativeInterval - 1; i++)
			{
				negativeResult.Enqueue(item: true);
			}
		}

		public void InitialPositiveData()
		{
			positiveResult = new Queue<bool>(positiveInterval);
			for (int i = 0; i < positiveInterval - 1; i++)
			{
				positiveResult.Enqueue(item: true);
			}
		}

		public void ClearPositiveData()
		{
			positiveResult.Clear();
		}

		public void ClearNegativeData()
		{
			negativeResult.Clear();
		}

		public void JoinPositiveData(bool falg)
		{
			lock (lock_positive)
			{
				if (positiveResult.Count < positiveInterval)
				{
					positiveResult.Enqueue(falg);
				}
			}
		}

		public bool OutPositiveData()
		{
			bool flag = false;
			if (positiveResult.Count >= positiveInterval)
			{
				return positiveResult.Dequeue();
			}
			return true;
		}

		public void JoinNegativeData(bool result)
		{
			lock (lock_negative)
			{
				if (negativeResult.Count < negativeInterval)
				{
					negativeResult.Enqueue(result);
				}
			}
		}

		public bool OutNegativeData()
		{
			bool flag = false;
			if (negativeResult.Count >= negativeInterval)
			{
				return negativeResult.Dequeue();
			}
			return true;
		}
	}

}