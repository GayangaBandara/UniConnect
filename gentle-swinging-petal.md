# UniConnect.Api - Complete Project Roadmap

## Project Vision
**UniConnect.Api** is an educational mentorship platform connecting students with mentors. The platform enables students to post guidance requests, mentors to accept and respond to those requests, and both parties to collaborate on learning/skill development.

**Current State**: User & GuidanceRequest CRUD fully implemented with clean architecture
**Tech Stack**: ASP.NET Core 9.0, EF Core, SQL Server, JWT auth (to add)
**Overall Timeline**: 13-15 weeks (4-5 weeks per phase)

---

## PHASE 1: MVP FEATURES (Core Functionality) - 4-5 Weeks

The MVP creates a closed loop: Student posts → Mentor discovers → Mentor accepts → Request lifecycle complete.

### 1.1 Authentication & Authorization (CRITICAL BLOCKER)
**Why**: Without authentication, system is insecure and all other features are vulnerable.

**Implementation**:
- JWT token generation with refresh tokens
- Login endpoint (email/password)
- Better password hashing (BCrypt with salt, replace current SHA256)
- [Authorize] attributes on all protected endpoints
- Role-based access control (RBAC) - student/mentor/admin roles

**Files to Create**:
- `Services/AuthenticationService.cs` - JWT logic, password hashing
- `Controllers/AuthController.cs` - Login/Register endpoints
- `DTOs/Auth/LoginRequestDto.cs`, `LoginResponseDto.cs`

**Files to Modify**:
- `Models/User.cs` - Add PasswordSalt field
- `Program.cs` - Add JWT auth scheme
- All controllers - Add [Authorize] attributes

---

### 1.2 Input Validation & Error Handling
**Why**: Production quality backend requires consistent error responses and validation.

**Implementation**:
- Global exception middleware (catch all unhandled exceptions)
- Custom exception classes (NotFoundException, UnauthorizedException, ForbiddenException)
- Standardized ApiResponse format for all responses
- Data annotations for DTO validation
- Input sanitization

**Files to Create**:
- `Middleware/GlobalExceptionMiddleware.cs`
- `Exceptions/CustomExceptions.cs`
- `Models/ApiResponse.cs`

**Files to Modify**:
- `Program.cs` - Add exception middleware

---

### 1.3 Mentor Profile Enhancements
**Why**: Mentors need profiles showcasing expertise; students need to evaluate mentors.

**Model Changes (User entity)**:
- `Expertise` (string) - topics they mentor on (comma-separated or JSON)
- `Bio` (string) - profile description
- `HourlyRate` (decimal?) - optional
- `IsAvailable` (bool) - availability status

**DTOs Update**:
- Include Expertise, Bio, IsAvailable in UserCreateDto, UserUpdateDto, UserResponseDto

**Files to Modify**:
- `Models/User.cs` - Add new fields
- `DTOs/User/*` - Update all user DTOs
- `Migrations/` - New migration for schema changes

---

### 1.4 Search & Filtering Guidance Requests
**Why**: Students/Mentors need to discover relevant requests; basic MVP discovery.

**Repository Enhancements (GuidanceRequestRepository)**:
- `GetByStatusAsync(status)` - Filter by pending/accepted/closed
- `GetByTopicAsync(topic)` - Category-based search
- `GetByMentorIdAsync(mentorId)` - Mentor's accepted requests
- `GetByStudentIdAsync(studentId)` - Student's own requests
- `GetPagedAsync(pageNumber, pageSize)` - Pagination

**Controller Enhancements**:
- GET `/api/requests?status=pending&topic=CSharp&page=1&pageSize=10`
- GET `/api/requests/mentor/{mentorId}` - Mentor's requests
- GET `/api/requests/student/{studentId}` - Student's requests

**Model Changes**:
- Add `Category` (string) field to GuidanceRequest

**Files to Modify**:
- `Repositories/GuidanceRequestRepository.cs`
- `Services/GuidanceRequestService.cs`
- `Controllers/GuidanceRequestsController.cs`
- `DTOs/GuidanceRequest/*` - Add Category field
- `Models/GuidanceRequest.cs` - Add Category field

---

### 1.5 Request Matching System (Mentor Acceptance)
**Why**: Core feature - mentors accepting student requests.

**Implementation**:
- Explicit mentor acceptance endpoint: POST `/api/requests/{id}/accept`
- Automatic status change: pending → accepted
- Permission check: Only the mentor specified can accept
- Validation: Mentor must exist, have relevant expertise

**Business Logic**:
- Service validates mentor exists, is available, request is pending
- Service sets MentorId and updates status
- Only mentor who accepted can later update/close

**Files to Modify**:
- `Services/GuidanceRequestService.cs` - Add AcceptRequestAsync logic
- `Controllers/GuidanceRequestsController.cs` - Add POST /accept endpoint

---

### 1.6 Request Closure (Complete Lifecycle)
**Why**: Allow students/mentors to mark requests as completed.

**Implementation**:
- Endpoint: PUT `/api/requests/{id}/close`
- Status change: accepted → closed
- Add `CompletedAt` timestamp
- Permission: Only student or assigned mentor can close

**Files to Modify**:
- `Models/GuidanceRequest.cs` - Add CompletedAt field
- `Services/GuidanceRequestService.cs` - Add CloseRequestAsync logic
- `Controllers/GuidanceRequestsController.cs` - Add PUT /close endpoint

---

### 1.7 Structured Logging
**Why**: Production debugging and monitoring; capture auth attempts, key operations, errors.

**Implementation**:
- Use ILogger (built-in) or Serilog (more powerful)
- Log authentication attempts
- Log CRUD operations
- Log all exceptions
- Structured logging with context (request ID, user ID, etc.)

**Files to Modify**:
- `Program.cs` - Add logging configuration
- `appsettings.json` - Add logging levels
- Services - Inject ILogger and log key operations

---

### Phase 1 Deliverables Checklist
- [ ] User registration (student/mentor) with validation
- [ ] User login with JWT token (25-30 min expiry, refresh token support)
- [ ] Students can post guidance requests with category/topic
- [ ] Mentors can search/filter requests by status, topic, recency
- [ ] Mentors can accept requests (status: pending → accepted)
- [ ] Students/Mentors can close requests (status: accepted → closed)
- [ ] All endpoints return standardized error responses
- [ ] Global exception handling (no stack traces exposed)
- [ ] Structured logging implemented
- [ ] Swagger/OpenAPI documentation complete
- [ ] Password hashing secure (BCrypt)
- [ ] Role-based access control working
- [ ] **Can deploy to test environment**

---

## PHASE 2: POST-MVP FEATURES (Communication & Discovery) - 4-5 Weeks

### 2.1 Email Notifications
- Trigger when request is posted (notify relevant mentors)
- Trigger when request is accepted (notify student)
- Trigger when request is closed (notify both parties)
- Email service integration (SendGrid, AWS SES, etc.)
- Notification preference management

### 2.2 In-App Notifications
- Notification entity in database
- Mark as read/unread
- Notification history
- Dashboard showing recent notifications

### 2.3 Direct Messaging
- Message entity (FromUserId, ToUserId, Content, Timestamp, IsRead)
- Conversation endpoints: GET conversations, GET conversation history
- Mark messages as read
- Delete messages (soft delete)

### 2.4 Mentor Discovery Page
- GET `/api/mentors` - list all mentors with profiles
- GET `/api/mentors?expertise=CSharp&minRating=4.0` - filter mentors
- Show: Name, Expertise, Bio, Availability, (Rating once added in Phase 3)

### 2.5 Advanced Request Features
- Request categories/topics (predefined list)
- Request expiration (auto-expire pending after N days)
- Full-text search on request title/description
- Sort options: date, popularity, mentor rating

### Phase 2 Deliverables Checklist
- [ ] Email notifications functional (SMTP provider configured)
- [ ] In-app notifications visible and readable
- [ ] Direct messaging between any two users working
- [ ] Mentor discovery/filtering page functional
- [ ] Advanced search operators working
- [ ] Integration tests passing (50%+ coverage)
- [ ] Can handle 100 concurrent users
- [ ] **Can deploy to staging environment**

---

## PHASE 3: PRODUCTION-READY & ADVANCED FEATURES - 4-5 Weeks

### 3.1 Reviews & Ratings System
- Review entity: ReviewerId, TargetUserId, RequestId, Rating (1-5), Comment
- Mentors reviewed by students (after request closes)
- Students reviewed by mentors (after request closes)
- Average rating per user, displayed on profile
- Review moderation (admin can delete)

### 3.2 Admin Analytics Dashboard
- Total users, mentors, students active
- Total requests: posted, completed, pending
- Average rating, completion rate
- Response time metrics (time from post to acceptance)
- Monthly/quarterly reports

### 3.3 Security Hardening
- Rate limiting on login endpoint (prevent brute force)
- Rate limiting on API endpoints (prevent scraping)
- CORS configuration for frontend domain
- Password strength requirements enforcement
- Session security headers (HSTS, CSP, etc.)

### 3.4 Performance Optimization
- Database indexing strategy
- Caching layer (Redis) for frequently accessed data
- Query optimization (N+1 prevention)
- Load testing with 1000 concurrent users
- Target: p95 response time <500ms

### 3.5 Deployment & Operations
- CI/CD pipeline setup (GitHub Actions or Azure DevOps)
- Automated database backups
- Monitoring & alerting (Application Insights or similar)
- Error tracking (Sentry or similar)
- Log aggregation and analysis

### Phase 3 Deliverables Checklist
- [ ] Reviews & ratings fully functional
- [ ] Admin dashboard operational
- [ ] 80%+ code coverage with tests
- [ ] Load testing passed (1000 concurrent users)
- [ ] Security audit completed
- [ ] Database backups automated
- [ ] Monitoring & alerting configured
- [ ] Deployment documentation complete
- [ ] **Production-ready release**

---

## CRITICAL IMPLEMENTATION SEQUENCE

### Week 1: Authentication & Security Foundation
```
1. JWT authentication setup → UNBLOCKS all other work
2. Password hashing improvement (SHA256 → BCrypt)
3. Login/Register endpoints
4. [Authorize] attributes on existing endpoints
```

### Week 2: Error Handling & Validation
```
1. Global exception middleware
2. Custom exceptions + standardized response format
3. Input validation with data annotations
4. Structured logging setup (Serilog)
```

### Week 3: Discovery Features (Parallel Path)
```
Path A: Mentor Profiles + Filtering
  - User model enhancements (Expertise, Bio, IsAvailable)
  - Repository filter methods

Path B: Search & Sorting
  - Request category addition
  - Pagination implementation
```

### Week 4: Request Lifecycle
```
1. Mentor acceptance endpoint
2. Request closure endpoint
3. Permission/authorization logic
4. Status validation
```

### Week 5: Testing & Polish
```
1. Unit tests for all services (70%+ coverage)
2. Integration tests for key flows
3. Swagger documentation complete
4. Performance baseline testing
```

---

## CRITICAL FILES TO MODIFY/CREATE

### High Priority (Phase 1)
1. **`Services/AuthenticationService.cs`** (CREATE) - JWT generation, password hashing, token validation
2. **`Controllers/AuthController.cs`** (CREATE) - Login, Register, Refresh endpoints
3. **`Middleware/GlobalExceptionMiddleware.cs`** (CREATE) - Global error handling
4. **`Exceptions/CustomExceptions.cs`** (CREATE) - Custom exception classes
5. **`DTOs/Auth/LoginRequestDto.cs`** (CREATE)
6. **`DTOs/Auth/LoginResponseDto.cs`** (CREATE)
7. **`Models/User.cs`** (MODIFY) - Add PasswordSalt, Expertise, Bio, IsAvailable
8. **`Models/GuidanceRequest.cs`** (MODIFY) - Add Category, CompletedAt
9. **`Program.cs`** (MODIFY) - Auth scheme, middleware registration, dependency injection
10. **`Repositories/GuidanceRequestRepository.cs`** (MODIFY) - Add filter methods
11. **`Services/GuidanceRequestService.cs`** (MODIFY) - Add validation and permission logic
12. **`Controllers/GuidanceRequestsController.cs`** (MODIFY) - Add accept/close endpoints

### Medium Priority (Phase 2)
13. **`Models/Notification.cs`** (CREATE)
14. **`Models/Message.cs`** (CREATE)
15. **`Services/NotificationService.cs`** (CREATE)
16. **`Controllers/NotificationController.cs`** (CREATE)
17. **`Controllers/MessageController.cs`** (CREATE)

### Phase 3
18. **`Models/Review.cs`** (CREATE)
19. **`Services/ReviewService.cs`** (CREATE)
20. **`Controllers/ReviewController.cs`** (CREATE)
21. **`Controllers/AdminController.cs`** (CREATE) - Analytics endpoints

---

## CRITICAL DECISIONS - CONFIRMED

### 1. Authentication Method
- **Decision**: ✅ **JWT + Refresh Tokens** (stateless, scales well)
- **Implementation**: Access tokens expire in 25-30 min, refresh tokens for long-lived sessions
- **Impact**: Frontend stores JWT in secure HttpOnly cookie or localStorage

### 2. Email Notification Provider
- **Decision**: ✅ **SendGrid**
- **Implementation**: API key-based integration, templates for request notifications
- **Timeline**: Phase 2 onwards (after core MVP complete)
- **Setup**: NuGet package: SendGrid, configure API key in appsettings

### 3. Real-Time Notifications Strategy
- **Decision**: ✅ **Email notifications for MVP** (Phase 1-2)
- **Future**: SignalR can be added in Phase 3 for real-time features
- **Impact**: Simple implementation, users check email for updates
- **Scalability**: Stateless, no persistent connections needed

### 4. Deployment Platform
- **Decision**: ✅ **Azure App Service**
- **Implementation**: Pair with Azure SQL Server for database
- **CI/CD**: Azure DevOps Pipelines or GitHub Actions
- **Phase 3 Task**: Set up automated deployments with database migrations
- **Cost**: Monitor spending, use dev/staging/prod environments

### 5. Compliance Requirements
- **Decision**: ✅ **GDPR Required**
- **Implementation Details**:
  - Data retention policy: Users can request data deletion (implement in Phase 2+)
  - Hard delete user data: Cascade delete guidance requests
  - Privacy policy: Create and display in app
  - Consent: Get explicit consent on registration for data processing
  - Audit logs: Log data access for compliance
- **Database Strategy**: Avoid soft deletes for now, use hard deletes to respect GDPR
- **Personal Data**: Email, Full Name, Bio are personal data - need proper protection

### 6. Performance Targets
- **Decision**: ✅ **Scalability Path**:
  - MVP (Phase 1): Support 50-100 concurrent users
  - Phase 2: Scale to 100-500 concurrent users
  - Phase 3: Scale to 1000+ concurrent users
- **Response Time SLA**: P95 < 500ms for normal operations
- **Database**: Start with single Azure SQL, monitor for scale-out needs

---

## RISK FACTORS & MITIGATION

### High-Risk
| Risk | Impact | Mitigation |
|------|--------|-----------|
| Auth implementation delays | All features blocked | Make Week 1 priority, clear requirements upfront |
| Weak password security | User data compromised | Use BCrypt immediately, not SHA256 |
| Database migration failures | Data loss, downtime | Test migrations on copy, have rollback plan |
| Email service misconfiguration | Users don't receive notifications | Set up early in Phase 2, test thoroughly |

### Medium-Risk
| Risk | Impact | Mitigation |
|------|--------|-----------|
| Performance degrades at scale | Poor UX with many users | Start load testing in Phase 2 |
| Search becomes slow | Mentors can't find requests | Add full-text indexing by Phase 3 |
| Testing coverage low | Regressions in production | Set 70%+ unit test requirement Phase 1 |

---

## VERIFICATION & TESTING STRATEGY

### Phase 1 End-to-End Test (MVP Validation)
```
1. Register as student (verify email valid, password hashed)
2. Register as mentor (add expertise, bio)
3. Login as student (receive JWT token)
4. Student posts guidance request (category: "C#")
5. Logout student, login as mentor
6. Mentor searches requests (filter by category "C#")
7. Mentor accepts request (status → accepted, MentorId set)
8. Logout mentor, login as student
9. Student closes request (status → closed, CompletedAt set)
10. Both users can see closed request in history
11. Test error: Unauthenticated user cannot create request (401)
12. Test error: Invalid category returns validation error (400)
```

### Phase 2 Test Additions
```
1. Email received when request posted
2. Email received when request accepted
3. Send message between student and mentor
4. Retrieve conversation history
5. List mentors with expertise filter
6. Mark notification as read
```

### Phase 3 Test Additions
```
1. Student rates mentor (1-5 stars + comment)
2. Mentor average rating updates
3. Admin views analytics dashboard (total requests, completion rate)
4. Load test: 1000 concurrent users creating requests
5. Search query returns results in <500ms
```

---

## EFFORT BREAKDOWN (Person-Weeks)

| Component | Effort | Dependencies |
|-----------|--------|--------------|
| Phase 1: Authentication | 1 week | None (BLOCKER) |
| Phase 1: Error/Validation | 3-4 days | Auth complete |
| Phase 1: Mentor Profiles | 2-3 days | Auth complete |
| Phase 1: Search/Filtering | 3-4 days | Auth complete |
| Phase 1: Request Matching | 2-3 days | Search complete |
| Phase 1: Request Closure | 1 day | Matching complete |
| Phase 1: Logging | 1-2 days | Can run in parallel |
| Phase 1: Testing/Docs | 1 week | All features complete |
| **Phase 1 Total** | **4-5 weeks** | |
| **Phase 2 Total** | **4-5 weeks** | Phase 1 complete |
| **Phase 3 Total** | **4-5 weeks** | Phase 2 complete |
| **Grand Total** | **13-15 weeks** | |

---

## ARCHITECTURE PRINCIPLES TO MAINTAIN

1. **Clean Separation**: Controllers → Services → Repositories → DbContext
2. **Dependency Injection**: All dependencies via constructor
3. **DTOs**: All entities have create/update/response DTOs
4. **Async/Await**: All I/O operations async
5. **Interfaces**: All services and repositories have interfaces
6. **Migrations**: One migration per meaningful schema change

---

## SUCCESS CRITERIA

**MVP (Phase 1) Success**:
- Students and mentors can complete at least one full cycle (post → accept → close)
- Zero unhandled exceptions in API responses
- Can securely authenticate any user
- All endpoints documented in Swagger

**Production-Ready (Phase 3) Success**:
- Can handle 1000 concurrent users without timeouts
- 80%+ test coverage
- Zero security vulnerabilities in audit
- Complete operational documentation
- Ready for real users

---

## Next Steps

1. Answer the Critical Decisions section above
2. Assign team members to Phase 1 tracks
3. Begin Week 1: Authentication implementation
4. Weekly sync on blockers
5. Phase 1 review/demo at week 5
