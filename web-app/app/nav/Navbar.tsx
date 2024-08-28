import { getCurrentUser } from "../actions/authAction"
import LoginButton from "./LoginButton"
import UserActions from "./UserActions"

export default async function Navbar(){
    const user = await getCurrentUser()
    return(
        <header className='stick top-0 z-50 flex justify-between bg-white p-3 item-center text-gray-800 shadow-md'>
            <div className="flex w-full">
            <div className="cursor-pointer">
                <a href="/">
                Typist
                </a>
            </div>
            <div className="flex ml-auto">
                <div className="ml-6 mr-6 cursor-pointer">online competition</div>
                {user ? (<UserActions />) : <LoginButton />}
            </div>
            </div>
        </header>
    )
}
