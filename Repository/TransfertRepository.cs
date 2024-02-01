using System;
using AutoMapper;
using ForSendKH.Contracts;
using ForSendKH.Data.DataContext;
using ForSendKH.Exceptions;
using ForSendKH.Models;
using ForSendKH.Models.ModelsDto.Transfert;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForSendKH.Repository
{
    public class TransfertRepository : GenericRepository<Transfert>, ITransfertRepository
    {
        private readonly TransfertDbContext _context;
        public TransfertRepository(TransfertDbContext context,IMapper mapper) : base(context,mapper)
        {
            this._context = context;
        }

        public void InitializeData(Transfert transfert)
        {
            var rand = new Random();
            transfert.DateDuTransfert = DateTime.Now;
            transfert.EtatDuTransfert = "Transfere";
            transfert.CodeDeRetrait = RandomString(8);
            transfert.Commission = CommissionTransfert(transfert, 5);
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).
                Select(s => s[random.Next(s.Length)]).ToArray()
                );
        }

        public static double CommissionTransfert(Transfert transfert, double pourcentageCommission)
        {
            double commmission = 0.0;

            if (transfert.Montant > 0.0)
            {
                commmission = (transfert.Montant * pourcentageCommission) / 100;
            }

            return commmission;
        }

        public async Task<Transfert> GetFerifyCode(string codeTransfert)
        {
            return await _context.Transferts
                .Where(p => p.CodeDeRetrait == codeTransfert)
                .FirstOrDefaultAsync();
               
        }

        public async Task<Transfert> GetVerifyEtat(string codeTransfert)
        {
            return await _context.Transferts
                .Where(p => p.EtatDuTransfert == "Transmis")
                .Where(p => p.CodeDeRetrait == codeTransfert)
                .FirstOrDefaultAsync();
        }

        public void Init(Transfert transfert)
        {
            transfert.DateDeTransmission = DateTime.Now;
            transfert.EtatDuTransfert = "Transmis";
        }

    }
}

