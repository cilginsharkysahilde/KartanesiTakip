using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takip.Repository
{
    public class RepositoryWrapper
    {
        private RepositoryContext context;

        private CustomerRepository customerRepository;
        private ProductRepository productRepository;
        private PersonnelRepository personnelRepository;
        private RoleRepository roleRepository;
        private CategoryRepository categoryRepository;
        private AddressRepository addressRepository;
        private ServiceRepository serviceRepository;


        public RepositoryWrapper(RepositoryContext context)
        {
            this.context = context;
        }

        public CustomerRepository CustomerRepository 
        { 
            get 
            { if (customerRepository == null)
                    customerRepository = new CustomerRepository(context);
                return customerRepository;
            } 
        }

        public AddressRepository AddressRepository
        {
            get
            {
                if (addressRepository == null)
                    addressRepository = new AddressRepository(context);
                return addressRepository;
            }
        }

        public ServiceRepository ServiceRepository
        {
            get
            {
                if (serviceRepository == null)
                    serviceRepository = new ServiceRepository(context);
                return serviceRepository;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(context);
                return productRepository;
            }
        }

        public PersonnelRepository PersonnelRepository
        {
            get
            {
                if (personnelRepository == null)
                    personnelRepository = new PersonnelRepository(context);
                return personnelRepository;
            }
        }

        public RoleRepository RoleRepository
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(context);
                return roleRepository;
            }
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(context);
                return categoryRepository;
            }
        }




        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
