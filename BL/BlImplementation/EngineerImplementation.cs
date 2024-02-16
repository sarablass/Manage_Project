using BlApi;
namespace BlImplementation
{
    /// <summary>
    /// Implementation of the IEngineer interface.
    /// </summary>
    internal class EngineerImplementation : IEngineer
    {
        private DalApi.IDal _dal = DalApi.Factory.Get; // Instance of the dal.

        /// <summary>
        /// Creates a new engineer.
        /// </summary>
        /// <param name="boEngineer">The engineer object to create.</param>
        /// <returns>The ID of the new created engineer.</returns>
        public int Create(BO.Engineer boEngineer)
        {
            // Validate engineer properties.
            BO.Tools.ValidationId(boEngineer.Id);
            BO.Tools.ValidationString(boEngineer.Name!);
            BO.Tools.ValidationEmail(boEngineer.Email!);
            BO.Tools.ValidationCost((double)boEngineer.Cost!);

            // Map BO.Engineer to DO.Engineer.
            DO.Engineer doEngineer = new DO.Engineer(
                boEngineer.Id,
                boEngineer.Name!,
                boEngineer.Email!,
                (DO.EngineerExperience)boEngineer.Level!,
                (double)boEngineer.Cost!,
                boEngineer.IsActive
            );
            // Create the engineer in the dal.
            try
            {
                int idEng = _dal.Engineer.Create(doEngineer);
                return idEng;// Returning the id of the new engineer.
            }
            catch (DO.DalAlreadyExistsException ex)// Throwing an exception in case such an engineer already exists.
            {
                throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
            }
        }

        /// <summary>
        /// Deletes an engineer with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the engineer to delete.</param>
        public void Delete(int id)
        {
            throw new BO.BlDeletionImpossible($"Engineer with ID={id} cannot be deleted");// An exception throw that cannot be deleted from an engineer.
        }

        /// <summary>
        /// Reads an engineer with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the engineer to read.</param>
        /// <returns>The engineer object.</returns>
        public BO.Engineer? Read(int id)
        {
            // Read engineer from the dal.
            DO.Engineer? doEngineer = _dal.Engineer.Read(id);

            // If engineer does not exist, throw exception.
            if (doEngineer is null)
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");

            // Read task associated with the engineer.
            DO.Task task = _dal.Task.ReadAll().FirstOrDefault(task => task?.EngineerId == doEngineer.Id)!;

            // Map DO.Engineer to BO.Engineer.
            return new BO.Engineer()
            {
                Id = doEngineer.Id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Level = (BO.EngineerExperience)doEngineer.Level!,
                Cost = (double)doEngineer.Cost!,
                Task = task != null ? new BO.TaskInEngineer()
                { Id = task!.Id, Alias = task.Alias! } : null
            };
        }

        /// <summary>
        /// Reads all engineers optionally filtered by a predicate.
        /// </summary>
        /// <param name="filter">Optional predicate to filter engineers.</param>
        /// <returns>A collection of engineers.</returns>
        public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
        {
            // Converting the engineers from the dl to bl.
            IEnumerable<BO.Engineer?> boEngineersList =
                from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                let task = _dal.Task.ReadAll(task => task?.EngineerId == doEngineer.Id).FirstOrDefault()
                select new BO.Engineer()
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level!,
                    Cost = (double)doEngineer.Cost!,
                    Task = task != null ? new BO.TaskInEngineer()
                    { Id = task!.Id, Alias = task.Alias! } : null
                };

            // Apply filter if provided.
            if (filter is null)
                return boEngineersList!;
            return boEngineersList.Where(filter!)!;
        }

        /// <summary>
        /// Updates an existing engineer.
        /// </summary>
        /// <param name="boEngineer">The engineer object to update.</param>
        public void Update(BO.Engineer boEngineer)
        {
            // Validate engineer properties.
            BO.Tools.ValidationId(boEngineer.Id);
            BO.Tools.ValidationString(boEngineer.Name!);
            BO.Tools.ValidationEmail(boEngineer.Email!);
            BO.Tools.ValidationCost((double)boEngineer.Cost!);

            // Map BO.Engineer to DO.Engineer. 
            DO.Engineer doEngineer = new DO.Engineer(
                boEngineer.Id,
                boEngineer.Name!,
                boEngineer.Email!,
                (DO.EngineerExperience)boEngineer.Level!,
                (double)boEngineer.Cost!,
                boEngineer.IsActive
            );
            // Update the engineer in the dal.
            try
            {
                _dal.Engineer.Update(doEngineer);
            }
            catch (DO.DalDoesNotExistException ex) // throw exception if engineer does not exist.
            {
                throw new BO.BlDoesNotExistException(ex.Message);
            }
        }
    }
}