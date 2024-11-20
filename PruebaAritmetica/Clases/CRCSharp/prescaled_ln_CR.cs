using System.Collections.Concurrent;
using System.Numerics;

namespace PruebaAritmetica.Clases.CRCSharp
{
    internal class prescaled_ln_CR : slow_CR
    {
        CR _x;

        public prescaled_ln_CR(CR x)
        {
            _x = x;
        }

        public override BigInteger approximate(int prec)
        {
            if (prec >= 0) return big0;
            int iterations_needed = -prec;
            int calc_precision = prec - bound_log2(2 * iterations_needed) - 4;
            int op_prec = prec - 3;
            BigInteger op_appr = _x.get_appr(op_prec);
            BigInteger x_nth = scale(op_appr, op_prec - calc_precision);
            BigInteger current_term = x_nth;
            BigInteger current_sum = current_term;

            int n = 1;
            int current_sign = 1;
            BigInteger max_trunc_error = big1 << (prec - 4 - calc_precision);
            while (BigInteger.Abs(current_term) >= max_trunc_error)
            {
                // ******************************************************
                // Implementar Token de cancelación
                // ******************************************************
                current_sign = -current_sign;
                x_nth = scale(x_nth * op_appr, op_prec);
                current_term = x_nth / (++n * current_sign);
                current_sum += current_term;
            }
            return scale(current_sum, calc_precision - prec);
        }

        //public override BigInteger approximate(int prec)
        //{
        //    if (prec >= 0) return big0;
        //    int iterations_needed = -prec;
        //    int calc_precision = prec - bound_log2(2 * iterations_needed) - 4;
        //    int op_prec = prec - 3;
        //    BigInteger op_appr = _x.get_appr(op_prec);
        //    BigInteger x_nth = scale(op_appr, op_prec - calc_precision);
        //    BigInteger current_sum = x_nth;

        //    int n = 1;
        //    int current_sign = 1;
        //    BigInteger max_trunc_error = big1 << (prec - 4 - calc_precision);

        //    var termsQueue = new ConcurrentQueue<BigInteger>();
        //    var cancellationTokenSource = new System.Threading.CancellationTokenSource();
        //    var token = cancellationTokenSource.Token;

        //    // Ciclo paralelo 1
        //    var producerTask = Task.Run(() =>
        //    {
        //        while (!token.IsCancellationRequested)
        //        {
        //            x_nth = scale(x_nth * op_appr, op_prec);
        //            termsQueue.Enqueue(x_nth);
        //        }
        //    }, token);

        //    // Ciclo paralelo 2
        //    var consumerTask = Task.Run(() =>
        //    {
        //        BigInteger current_term;
        //        while (!token.IsCancellationRequested)
        //        {
        //            if (termsQueue.TryDequeue(out BigInteger x_nth_res))
        //            {
        //                current_sign = -current_sign;
        //                current_term = x_nth_res / (++n * current_sign);
        //                current_sum += current_term;
        //                if (BigInteger.Abs(current_term) < max_trunc_error)
        //                {
        //                    cancellationTokenSource.Cancel();
        //                }
        //            }
        //        }
        //    }, token);

        //    Task.WaitAll(producerTask, consumerTask);

        //    return scale(current_sum, calc_precision - prec);
        //}

    }
}
